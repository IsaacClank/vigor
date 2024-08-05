using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace User.Database;

public class UserDbContextDesignTimeFactory : IDesignTimeDbContextFactory<UserDbContext>
{
  public UserDbContext CreateDbContext(string[] args)
  {
    DbContextOptionsBuilder<UserDbContext> optionsBuilder = new();
    optionsBuilder.UseNpgsql(args[0]);
    return new(optionsBuilder.Options);
  }
}
