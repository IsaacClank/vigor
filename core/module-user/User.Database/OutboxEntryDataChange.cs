using System.Text.Json.Serialization;

namespace User.Database
{
  public class OutboxEntryDataChange
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
  }
}
