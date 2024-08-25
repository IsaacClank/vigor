using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Vigor.Core.Common.Db.PostgreSql;

public class PostgreSqlDbContextDesignTimeFactory<T> : IDesignTimeDbContextFactory<T> where T : PostgreSqlDbContext
{
  protected const string DbConnectionVariable = "DB_CONNECTION";

  public T CreateDbContext(string[] args)
  {
    var dbConnection = Environment.GetEnvironmentVariable(DbConnectionVariable);

    if (string.IsNullOrWhiteSpace(dbConnection))
    {
      throw new ArgumentException($"{nameof(DbConnectionVariable)} is not set.");
    }

    DbContextOptionsBuilder<T> optionsBuilder = new();
    optionsBuilder.UseNpgsql(dbConnection).UseSnakeCaseNamingConvention();

    return (Activator.CreateInstance(typeof(T), [optionsBuilder.Options]) as T)!;
  }
}
