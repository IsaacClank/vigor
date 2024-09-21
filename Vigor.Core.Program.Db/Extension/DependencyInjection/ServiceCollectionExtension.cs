using Microsoft.Extensions.DependencyInjection;

using Vigor.Common.Db.PostgreSql.Extensions.DependencyInjection;

namespace Vigor.Core.Program.Db.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
  public static void AddProgramDbContext(
    this IServiceCollection services,
    string? connectionString) => services.AddPostgreSqlDbContext<ProgramDbContext>(connectionString);

  public static void AddProgramDbContextFactory(
    this IServiceCollection services,
    string? connectionString) => services.AddPostgreSqlDbContextFactory<ProgramDbContext>(connectionString);
}
