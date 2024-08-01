using User.Api.Queue;
using User.Common.Queue;
using User.Database;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddSingleton<QueueClient>((_) => new(builder.Configuration.GetConnectionString("Queue")));
builder.Services.AddHostedService<Consumer>();

var host = builder.Build();
host.Run();
