using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.Database
{
  public static class Library
  {
    public static void AddUserDbContext(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<UserDbContext>(o => o.UseNpgsql(connectionString));
    }
  }
}
