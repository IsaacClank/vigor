using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vigor.Common.Db.PostgreSql.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddPostgreSqlDbContext<T>(
    this IServiceCollection services,
    string? connectionString) where T : PostgreSqlDbContext
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new ArgumentNullException(
        nameof(connectionString),
        "Connection string cannot be null.");
    }

    services.AddDbContext<T>(o => o
      .UseNpgsql(connectionString)
      .UseSnakeCaseNamingConvention());
  }

  public static void AddPostgreSqlDbContextFactory<T>(
    this IServiceCollection services,
    string? connectionString) where T : PostgreSqlDbContext
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new ArgumentNullException(
        nameof(connectionString),
        "Connection string cannot be null.");
    }

    services.AddDbContextFactory<T>(o => o
      .UseNpgsql(connectionString)
      .UseSnakeCaseNamingConvention());
  }
}
