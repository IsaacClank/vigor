using User.Api.Queue;
using User.Database;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
