namespace Vigor.Core.Program.Domain.Facility.Interfaces;

public interface IFacilityCrud
{
  public Task<Contracts.Facility> UpsertAsync(Guid userId, Contracts.UpsertFacility upsertFacility);

  public Task<IEnumerable<Contracts.Facility>> FindAsync(Guid userId);

  public Task<Contracts.Facility> RemoveAsync(Guid userId, Guid facilityId);
}
