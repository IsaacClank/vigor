using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.Database
{
  public static class Library
  {
    public static void AddUserDbContext(this IServiceCollection services, string? connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString), "Database connection cannot be null.");
      }

      services.AddDbContext<UserDbContext>(o => o.UseNpgsql(connectionString));
    }
  }
}
