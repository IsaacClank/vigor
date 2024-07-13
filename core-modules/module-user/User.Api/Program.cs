using User.Database;

namespace User.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddUserDbContext();
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
        public static void AddUserDbContext(this IHostApplicationBuilder builder) => builder.Services.AddUserDbContext(
            builder.Configuration.GetConnectionString("UserDatabase")
                ?? throw new Exception("Database connection string cannot be null.")
        );
    }
}
