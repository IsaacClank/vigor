using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Database
{
  public static class Lib
  {
    public static void AddContractDbContext(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<ContractDbContext>(o => o.UseNpgsql(connectionString));
    }
  }
}
