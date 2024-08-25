using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Common.Db.PostgreSql.Extensions.DependencyInjection;

namespace Vigor.Core.User.Db.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
  public static void AddUserDbContext(
    this IServiceCollection services,
    string? connectionString) => services.AddPostgreSqlDbContext<UserDbContext>(connectionString);

  public static void AddUserDbContextFactory(
    this IServiceCollection services,
    string? connectionString) => services.AddPostgreSqlDbContextFactory<UserDbContext>(connectionString);
}
