using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vigor.Common.Db.Repository.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddScopedUnitOfWork(
    this IServiceCollection services,
    Func<IServiceProvider, DbContext> dbContextResolver)
      => services.AddScoped<IUnitOfWork, UnitOfWork>(
        p => new(dbContextResolver.Invoke(services.BuildServiceProvider())));
}
