using Microsoft.Extensions.DependencyInjection;

using Vigor.Common.Db.PostgreSql.Extensions.DependencyInjection;

namespace Vigor.Core.Platform.Db.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
  public static void AddPlatformDbContext(
    this IServiceCollection services,
    string? connectionString) => services.AddPostgreSqlDbContext<PlatformDbContext>(connectionString);

  public static void AddPlatformDbContextFactory(
    this IServiceCollection services,
    string? connectionString) => services.AddPostgreSqlDbContextFactory<PlatformDbContext>(connectionString);
}
