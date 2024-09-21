using Vigor.Common.JsonApi;

namespace Vigor.Core.Program.Domain.Facility.Contracts;

public class UpsertFacility : ContractBase
{
  public required string Name { get; set; }
  public string? Address { get; set; }
}
