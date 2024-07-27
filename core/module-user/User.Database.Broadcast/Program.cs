using User.Database;
using User.Database.Broadcast;
using User.Common.Queue;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddSingleton<QueueClient>((_provider) => new(
  builder.Configuration.GetConnectionString("Queue")
  ?? throw new ArgumentException("Queue connection string cannot be null.")));

builder.Services.AddHostedService<DatabaseBroadcaster>();

var host = builder.Build();
host.Run();
