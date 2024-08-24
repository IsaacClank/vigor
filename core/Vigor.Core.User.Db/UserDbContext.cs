using Microsoft.EntityFrameworkCore;

using Vigor.Core.Common.Db.PostgreSql;
using Vigor.Core.User.Db.Models;

namespace Vigor.Core.User.Db;

public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : PostgreSqlDbContext(options)
{
  public DbSet<Profile> Profile { get; set; }
}
