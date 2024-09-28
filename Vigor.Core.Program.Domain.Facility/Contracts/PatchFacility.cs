namespace Vigor.Core.Program.Domain.Facility.Contracts;

public class PatchFacility : Common.JsonApi.ContractBase
{
  public required new Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Address { get; set; }
}
