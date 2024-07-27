using System.Text.Json.Serialization;

namespace User.Database
{
  public class OutboxEntryData
  {
    [JsonPropertyName("change")]
    public IEnumerable<OutboxEntryDataChange> Changes { get; set; } = [];
  }
}
