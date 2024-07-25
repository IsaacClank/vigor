using Contract.Database;

namespace Contract.Api
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.AddContractDbContext();
      builder.Services.AddControllers();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();
      app.UseSwagger();
      app.UseSwaggerUI();
      app.MapControllers();
      app.Run();
    }
  }

  public static class ProgramExtensions
  {
    public static void AddContractDbContext(this IHostApplicationBuilder builder) => builder.Services.AddContractDbContext(
        builder.Configuration.GetConnectionString("ContractDatabase")
            ?? throw new ArgumentException("Database connection string cannot be null.")
    );
  }
}
