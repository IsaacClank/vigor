using System.Text.Json.Serialization;

namespace Vigor.Common.JsonApi;

public class DocumentDataEntry<T>(T contract) where T : ContractBase
{
  public string Type => typeof(T).Name;
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Id => Attributes.Id;
  public T Attributes { get; set; } = contract;
}
