using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace User.Database
{
  public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
  {
    public bool AnyOutboxEvent()
    {
      return Database
        .SqlQuery<IEnumerable<string>>($"SELECT lsn FROM pg_logical_slot_peek_changes('outbox', null, 1)")
        .Any();
    }

    public OutboxEntryContent NextOutboxEvent()
    {
      var result = Database.SqlQueryRaw<string>($"SELECT data as \"Value\" FROM pg_logical_slot_get_changes('outbox', null, 1)").Single();
      return JsonSerializer.Deserialize<OutboxEntryContent>(result) ?? throw new JsonException("Cannot deserialize outbox payload");
    }
  }
}
