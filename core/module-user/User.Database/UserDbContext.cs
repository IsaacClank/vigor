using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace User.Database
{
  public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
  {
    public object? GetNextLogicalSlotChange()
    {
      var x = Database.SqlQuery<string>($"SELECT data AS Value FROM pg_logical_slot_peek_changes('outbox', null, 1)").ToList().FirstOrDefault();

      if (x is null)
      {
        return x;
      }
      else
      {
        return JsonSerializer.Deserialize<object>(x);
      }
    }

    public void AcknowledgeLatestLogicalSlotChange()
    {
      Database.ExecuteSql($"SELECT data AS Value FROM pg_logical_slot_get_changes('outbox', null, 1)");
    }
  }
}
