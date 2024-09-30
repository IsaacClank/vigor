using System.ComponentModel.DataAnnotations;

using Vigor.Common.JsonApi;

namespace Vigor.Core.Platform.Domain.Facility.Contracts;

public class Facility : ContractBase
{
  [Required]
  public required string Name { get; set; }
  public string? Address { get; set; }
}
