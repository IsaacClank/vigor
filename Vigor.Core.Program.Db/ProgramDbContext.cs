using Microsoft.EntityFrameworkCore;

using Vigor.Core.Common.Db.PostgreSql;

namespace Vigor.Core.Program.Db;

public sealed class ProgramDbContext(DbContextOptions<ProgramDbContext> options) : PostgreSqlDbContext(options)
{
  public DbSet<Entities.Facility> Facility { get; set; }
  public DbSet<Entities.Membership> Membership { get; set; }
  public DbSet<Entities.Program> Program { get; set; }
}
