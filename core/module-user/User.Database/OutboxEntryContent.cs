using System.Text.Json.Serialization;

namespace User.Database
{
  public class OutboxEntryContent
  {
    [JsonPropertyName("change")]
    public IEnumerable<OutboxEntryChange> Changes { get; set; } = [];
  }
}
