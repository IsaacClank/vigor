using User.Api.Queue;
using User.Database;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase")
    ?? throw new ArgumentException("Database connection string cannot be null."));

var host = builder.Build();
host.Run();
