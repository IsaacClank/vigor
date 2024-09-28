namespace Vigor.Core.Program.Domain.Facility.Interfaces;

public interface IFacilityCrud
{
  public Task<Contracts.Facility> UpsertAsync(Guid userId, Contracts.UpsertFacility upsertFacility);

  public Task<IEnumerable<Contracts.Facility>> UpsertAsync(Guid userId, IEnumerable<Contracts.UpsertFacility> upsertFacilities);

  public Task<IEnumerable<Contracts.Facility>> PatchAsync(Guid userId, IEnumerable<Contracts.PatchFacility> patchFacilities);

  public Task<IEnumerable<Contracts.Facility>> FindAsync(Guid userId);

  public Task<Contracts.Facility> RemoveAsync(Guid userId, Guid facilityId);

  public Task<IEnumerable<Contracts.Facility>> RemoveAsync(Guid userId, IEnumerable<Guid> facilityIds);
}
