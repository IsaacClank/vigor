using System.Text.Json.Serialization;

namespace Vigor.Core.Common.Db.PostgreSql;

public class PostgreSqlOutboxEvent
{
  [JsonPropertyName("change")]
  public IEnumerable<PostgreSqlChange> Changes { get; } = [];

  public bool AnyChange()
  {
    return Changes.Any();
  }

  public IEnumerable<Dictionary<string, string>> ParseChanges() => Changes
    .Where(c => c.ColumnNames.Length > 0)
    .Select(c => c.ToDictionary());
}
