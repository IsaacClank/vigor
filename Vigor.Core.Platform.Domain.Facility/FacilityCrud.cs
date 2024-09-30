using System.Collections.Immutable;

using Microsoft.Extensions.Logging;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Common.Extensions.Logging;
using Vigor.Common.Extensions.System;
using Vigor.Common.Util;
using Vigor.Core.Platform.Domain.Facility.Contracts;
using Vigor.Core.Platform.Domain.Facility.Interfaces;

namespace Vigor.Core.Platform.Domain.Facility;

public class FacilityCrud(
  ILogger<FacilityCrud> logger,
  IUnitOfWork unitOfWork) : IFacilityCrud
{
  private ILogger<FacilityCrud> Logger { get; } = logger;
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
      var facilityToUpsert = Util.Map<Db.Entities.Facility>(upsertFacility);
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

    return upsertedFacilities.Select(facility => Util.Map<Contracts.Facility>(facility));
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

    return facilitiesDict.Values.Select(facility => Util.Map<Contracts.Facility>(facility));
  }

  public async Task<IEnumerable<Contracts.Facility>> FindAsync(Guid userId)
  {
    var facilities = await FacilityRepository.FindReadonlyAsync(f => f.OwnerId == userId);
    return facilities.Select(facility => Util.Map<Contracts.Facility>(facility));
  }

  public async Task<Contracts.Facility> RemoveAsync(Guid userId, Guid facilityId)
    => (await RemoveAsync(userId, [facilityId])).First();

  public async Task<IEnumerable<Contracts.Facility>> RemoveAsync(
    Guid userId,
    IEnumerable<Guid> facilityIds)
  {
    var facilities = await FacilityRepository
      .FindAsync(f => f.OwnerId == userId && facilityIds.Contains(f.Id));
    EntityNotFoundException.ThrowIfNull(facilities);

    facilities.ToList().ForEach(f => FacilityRepository.Delete(f));
    await _unitOfWork.SaveAsync();

    return facilities.Select(facility => Util.Map<Contracts.Facility>(facility));
  }
}
