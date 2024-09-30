using Microsoft.EntityFrameworkCore;

using Vigor.Common.Db.PostgreSql;

namespace Vigor.Core.Platform.Db;

public sealed class PlatformDbContext(DbContextOptions<PlatformDbContext> options) : PostgreSqlDbContext(options)
{
  public DbSet<Entities.Facility> Facility { get; set; }
  public DbSet<Entities.Membership> Membership { get; set; }
  public DbSet<Entities.TrainingPlan> TrainingPlan { get; set; }
}
