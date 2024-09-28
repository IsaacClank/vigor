using System.Collections.Immutable;

using AutoMapper;

using Microsoft.Extensions.Logging;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Common.Extensions.Logging;
using Vigor.Common.Extensions.System;
using Vigor.Common.Util;
using Vigor.Core.Program.Domain.Facility.Contracts;
using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Domain.Facility;

public class FacilityCrud(
  ILogger<FacilityCrud> logger,
  IMapper mapper,
  IUnitOfWork unitOfWork) : IFacilityCrud
{
  private ILogger<FacilityCrud> Logger { get; } = logger;
  private readonly IMapper _mapper = mapper;
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  private IRepository<Db.Entities.Facility> FacilityRepository
    => _unitOfWork.Repository<Db.Entities.Facility>();

  public async Task<Contracts.Facility> UpsertAsync(
    Guid userId,
    UpsertFacility upsertFacility)
    => (await UpsertAsync(userId, [upsertFacility])).First();

  public async Task<IEnumerable<Contracts.Facility>> UpsertAsync(
    Guid userId,
    IEnumerable<UpsertFacility> upsertFacilities)
  {
    List<Db.Entities.Facility> upsertedFacilities = [];
    await Parallel.ForEachAsync(upsertFacilities, async (upsertFacility, _) =>
    {
      var facilityToUpsert = _mapper.Map<Db.Entities.Facility>(upsertFacility);
      facilityToUpsert.OwnerId = userId;

      var upsertedFacility = upsertFacility.Id.IsEmptyOrDefault()
        ? await FacilityRepository.InsertAsync(facilityToUpsert)
        : FacilityRepository.Update(facilityToUpsert);

      upsertedFacilities.Add(upsertedFacility);
    });

    await _unitOfWork.SaveAsync();
    upsertedFacilities.ForEach(f => Logger.UpsertedEntity(
      nameof(Db.Entities.Facility),
      f.Id));

    return _mapper.Map<IEnumerable<Contracts.Facility>>(upsertedFacilities);
  }

  public async Task<IEnumerable<Contracts.Facility>> PatchAsync(
    Guid userId,
    IEnumerable<PatchFacility> patchFacilities)
  {
    var facilityIds = patchFacilities.Select(x => x.Id);
    var facilitiesDict = (await FacilityRepository
      .FindAsync(f => f.OwnerId == userId && facilityIds.Contains(f.Id)))
      .ToDictionary(f => f.Id, f => f);

    patchFacilities.ToList().ForEach(patchFacility =>
    {
      var facility = facilitiesDict[patchFacility.Id];
      Util.Map(
        facility,
        patchFacility,
        includedProperties:
        [
          nameof(PatchFacility.Name),
          nameof(PatchFacility.Address),
        ]);
    });
    await _unitOfWork.SaveAsync();

    return _mapper.Map<IEnumerable<Contracts.Facility>>(facilitiesDict.Values);
  }

  public async Task<IEnumerable<Contracts.Facility>> FindAsync(Guid userId)
  {
    var facilities = await FacilityRepository.FindReadonlyAsync(f => f.OwnerId == userId);
    return _mapper.Map<IEnumerable<Contracts.Facility>>(facilities);
  }

  public async Task<Contracts.Facility> RemoveAsync(Guid userId, Guid facilityId)
  {
    var facility = await FacilityRepository.FindAsync(facilityId);
    EntityNotFoundException.ThrowIfNull(facility);

    FacilityRepository.Delete(facility);
    await _unitOfWork.SaveAsync();
    Logger.DeletedEntity(nameof(Db.Entities.Facility), facilityId);

    return _mapper.Map<Contracts.Facility>(facility);
  }

  public async Task<IEnumerable<Contracts.Facility>> RemoveAsync(
    Guid userId,
    IEnumerable<Guid> facilityIds)
  {
    var facilities = await FacilityRepository
      .FindAsync(f => f.OwnerId == userId && facilityIds.Contains(f.Id));
    EntityNotFoundException.ThrowIfNull(facilities);

    facilities.ToList().ForEach(f => FacilityRepository.Delete(f));
    await _unitOfWork.SaveAsync();

    return _mapper.Map<IEnumerable<Contracts.Facility>>(facilities);
  }
}
