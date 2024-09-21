using System.Text.Json;

using Microsoft.EntityFrameworkCore;

namespace Vigor.Common.Db.PostgreSql;

public class PostgreSqlDbContext(
  DbContextOptions options) : DbContext(options)
{
  public bool AnyOutboxEvent()
  {
    return Database
      .SqlQuery<IEnumerable<string>>(
        $"SELECT lsn FROM pg_logical_slot_peek_changes('outbox', null, 1)")
      .Any();
  }

  public PostgreSqlOutboxEvent NextOutboxEvent()
  {
    var result = Database
      .SqlQueryRaw<string>(
        $"SELECT data as \"Value\" FROM pg_logical_slot_get_changes('outbox', null, 1)")
      .Single();

    return JsonSerializer.Deserialize<PostgreSqlOutboxEvent>(result)
      ?? throw new JsonException("Cannot deserialize outbox payload");
  }
}
