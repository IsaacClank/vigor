using AutoMapper;

using Microsoft.Extensions.Logging;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Common.Extensions.Logging;
using Vigor.Common.Extensions.System;
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
  {
    var programToUpserted = Mapper.Map<Db.Entities.Program>(upsertProgram);
    programToUpserted.OwnerId = userId;

    var upsertedProgram = upsertProgram.Id.IsEmptyOrDefault()
      ? await ProgramRepo.InsertAsync(programToUpserted)
      : ProgramRepo.Update(programToUpserted);
    await UnitOfWork.SaveAsync();
    Logger.UpsertedEntity(nameof(Db.Entities.Program), upsertedProgram.Id);

    return Mapper.Map<Contracts.Program>(upsertedProgram);
  }

  public async Task<IEnumerable<Contracts.Program>> FindAsync(Guid userId)
  {
    var programs = await ProgramRepo.FindAsync(p => p.OwnerId == userId);
    return Mapper.Map<IEnumerable<Contracts.Program>>(programs);
  }

  public async Task<Contracts.Program> RemoveAsync(Guid userId, Guid programId)
  {
    var programToDelete = await ProgramRepo.FindAsync(programId);
    EntityNotFoundException.ThrowIfNull(programToDelete);

    var deletedProgram = ProgramRepo.Delete(programToDelete);
    await UnitOfWork.SaveAsync();
    Logger.DeletedEntity(nameof(Db.Entities.Program), programId);

    return Mapper.Map<Contracts.Program>(deletedProgram);
  }
}
