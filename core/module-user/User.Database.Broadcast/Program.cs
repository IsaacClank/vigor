using User.Database;
using User.Database.Broadcast;
using User.Common.Queue;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddSingleton<QueueClient>((_) => new(builder.Configuration.GetConnectionString("Queue")));

builder.Services.AddHostedService<DatabaseBroadcaster>();

var host = builder.Build();
host.Run();
