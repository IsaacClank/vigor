using System.Text.Json.Serialization;

namespace Vigor.Common.JsonApi;

public abstract class ContractBase
{
  [JsonIgnore]
  public Guid? Id { get; set; }
}
