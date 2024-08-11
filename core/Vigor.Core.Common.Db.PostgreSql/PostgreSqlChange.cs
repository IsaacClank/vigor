using System.Text.Json.Serialization;

namespace Vigor.Core.Common.Db.PostgreSql;

public class PostgreSqlChange
{
  [JsonPropertyName("kind")]
  public string Kind { get; set; } = string.Empty;

  [JsonPropertyName("table")]
  public string Table { get; set; } = string.Empty;

  [JsonPropertyName("columnnames")]
  public string[] ColumnNames { get; set; } = [];

  [JsonPropertyName("columntypes")]
  public string[] ColumnTypes { get; set; } = [];

  [JsonPropertyName("columnvalues")]
  public object[] ColumnValues { get; set; } = [];

  public Dictionary<string, string> ToDictionary() => ColumnNames.Prepend("Type").Zip(ColumnValues.Prepend($"{Table}_{Kind}"))
    .Where(e => e.Second != null)
    .ToDictionary(e => e.First.ToString(), e => e.Second.ToString()) as Dictionary<string, string>;
}
