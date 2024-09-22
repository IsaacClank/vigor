using AutoMapper;

using Microsoft.Extensions.Logging;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Common.Extensions.Logging;
using Vigor.Common.Extensions.System;
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

  private IRepository<Db.Entities.Facility> FacilityRepository => _unitOfWork.Repository<Db.Entities.Facility>();

  public async Task<Contracts.Facility> UpsertAsync(
    Guid userId,
    UpsertFacility upsertFacility)
  {
    var facilityToUpsert = _mapper.Map<Db.Entities.Facility>(upsertFacility);
    facilityToUpsert.OwnerId = userId;

    var upsertedFacility = upsertFacility.Id.IsEmptyOrDefault()
      ? await FacilityRepository.InsertAsync(facilityToUpsert)
      : FacilityRepository.Update(facilityToUpsert);
    await _unitOfWork.SaveAsync();
    Logger.UpsertedEntity(nameof(Db.Entities.Facility), upsertedFacility.Id);

    return _mapper.Map<Contracts.Facility>(upsertedFacility);
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
}
