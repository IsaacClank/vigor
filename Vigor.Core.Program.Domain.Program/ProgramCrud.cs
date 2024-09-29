using AutoMapper;

using Microsoft.Extensions.Logging;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Common.Extensions.Logging;
using Vigor.Common.Extensions.System;
using Vigor.Common.Util;
using Vigor.Core.Program.Domain.Program.Contracts;
using Vigor.Core.Program.Domain.Program.Interfaces;

namespace Vigor.Core.Program.Domain.Program;

public class ProgramCrud(
  ILogger<ProgramCrud> logger,
  IMapper mapper,
  IUnitOfWork unitOfWork) : IProgramCrud
{
  private ILogger<ProgramCrud> Logger { get; set; } = logger;
  private IMapper Mapper { get; set; } = mapper;
  private IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
  private IRepository<Db.Entities.Program> ProgramRepo
    => UnitOfWork.Repository<Db.Entities.Program>();

  public async Task<Contracts.Program> UpsertAsync(
    Guid userId,
    UpsertProgram upsertProgram)
    => (await UpsertAsync(userId, [upsertProgram])).First();

  public async Task<IEnumerable<Contracts.Program>> UpsertAsync(
    Guid userId,
    IEnumerable<UpsertProgram> upsertPrograms)
  {
    List<Db.Entities.Program> upsertedPrograms = [];
    await Parallel.ForEachAsync(upsertPrograms, async (upsertProgram, _) =>
    {
      var programToUpsert = Mapper.Map<Db.Entities.Program>(upsertProgram);
      programToUpsert.OwnerId = userId;

      var upsertedProgram = upsertProgram.Id.IsEmptyOrDefault()
        ? await ProgramRepo.InsertAsync(programToUpsert)
        : ProgramRepo.Update(programToUpsert);

      upsertedPrograms.Add(upsertedProgram);
    });

    await UnitOfWork.SaveAsync();
    upsertedPrograms.ForEach(f => Logger.UpsertedEntity(
      nameof(Db.Entities.Program),
      f.Id));

    return Mapper.Map<IEnumerable<Contracts.Program>>(upsertedPrograms);
  }

  public async Task<IEnumerable<Contracts.Program>> PatchAsync(
    Guid userId,
    IEnumerable<PatchProgram> patchPrograms)
  {
    var programIds = patchPrograms.Select(x => x.Id);
    var programsDict = (await ProgramRepo
      .FindAsync(f => f.OwnerId == userId && programIds.Contains(f.Id)))
      .ToDictionary(f => f.Id, f => f);

    patchPrograms.ToList().ForEach(patchProgram =>
    {
      var program = programsDict[patchProgram.Id];
      Util.Map(
        program,
        patchProgram,
        includedProperties:
        [
          nameof(PatchProgram.Name),
          nameof(PatchProgram.MonthlyFee),
        ]);
    });
    await UnitOfWork.SaveAsync();

    return Mapper.Map<IEnumerable<Contracts.Program>>(programsDict.Values);
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Contracts.Program>> FindAsync(Guid userId)
  {
    var programs = await ProgramRepo.FindAsync(p => p.OwnerId == userId);
    return Mapper.Map<IEnumerable<Contracts.Program>>(programs);
  }

  public async Task<Contracts.Program> RemoveAsync(Guid userId, Guid programId)
    => (await RemoveAsync(userId, [programId])).First();

  public async Task<IEnumerable<Contracts.Program>> RemoveAsync(
    Guid userId,
    IEnumerable<Guid> programIds)
  {
    var programs = await ProgramRepo
      .FindAsync(f => f.OwnerId == userId && programIds.Contains(f.Id));
    EntityNotFoundException.ThrowIfNull(programs);

    programs.ToList().ForEach(f => ProgramRepo.Delete(f));
    await UnitOfWork.SaveAsync();

    return Mapper.Map<IEnumerable<Contracts.Program>>(programs);
  }
}
