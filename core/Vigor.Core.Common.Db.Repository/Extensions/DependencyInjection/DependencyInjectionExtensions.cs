using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vigor.Core.Common.Db.Repository.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
  public static void AddScopedUnitOfWork(this IServiceCollection services, Func<IServiceProvider, DbContext> dbContextResolver)
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>(p => new(dbContextResolver.Invoke(services.BuildServiceProvider())));
  }
}
