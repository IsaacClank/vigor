using AutoMapper;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Core.Program.Domain.Facility.Contracts;
using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Domain.Facility;

public class FacilityCrud(IMapper mapper, IUnitOfWork unitOfWork) : IFacilityCrud
{
  private readonly IMapper _mapper = mapper;
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  private IRepository<Db.Entities.Facility> FacilityRepository => _unitOfWork.Repository<Db.Entities.Facility>();

  public async Task<Contracts.Facility> UpsertAsync(Guid userId, UpsertFacility createFacility)
  {
    var facility = _mapper.Map<Db.Entities.Facility>(createFacility);
    facility.OwnerId = userId;

    if (facility.Id != Guid.Empty)
    {
      FacilityRepository.Update(facility);
    }
    else
    {
      FacilityRepository.Insert(facility);
    }

    await _unitOfWork.SaveAsync();
    return _mapper.Map<Contracts.Facility>(facility);
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
    return _mapper.Map<Contracts.Facility>(facility);
  }
}
