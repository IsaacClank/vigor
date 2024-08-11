using Vigor.Core.User.Db.Extensions.DependencyInjection;

namespace Vigor.Core.User.Api.Http
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
      builder.Services.AddControllers();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();
      app.UseSwagger();
      app.UseSwaggerUI();
      app.MapControllers();
      app.Run();
    }
  }
}
