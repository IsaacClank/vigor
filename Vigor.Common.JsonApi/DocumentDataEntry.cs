using System.Text.Json.Serialization;

namespace Vigor.Common.JsonApi;

public class DocumentDataEntry<T>(T contract) where T : ContractBase
{
  public string Type => typeof(T).Name;

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
  public string? Id => Attributes.Id?.ToString() ?? default;

  public T Attributes { get; set; } = contract;
}
