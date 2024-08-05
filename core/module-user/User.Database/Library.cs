using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.Database
{
  public static class Library
  {
    public static void AddUserDbContext(this IServiceCollection services, string? connectionString)
    {
      ValidateDbConnectionString(connectionString);
      services.AddDbContext<UserDbContext>(o => o.UseNpgsql(connectionString));
    }

    public static void AddUserDbContextFactory(this IServiceCollection services, string? connectionString)
    {
      ValidateDbConnectionString(connectionString);
      services.AddDbContextFactory<UserDbContext>(o => o.UseNpgsql(connectionString));
    }

    private static void ValidateDbConnectionString(string? connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString), "Database connection cannot be null.");
      }
    }
  }
}
