using User.Common.Queue;
using User.Database;
using User.Database.Broadcast;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddUserDbContextFactory(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddSingleton<QueueClient>((_) => new(builder.Configuration.GetConnectionString("Queue")));
builder.Services.AddSingleton<IQueuePublisher, QueuePublisher>(provider => new(
  provider.GetRequiredService<QueueClient>(),
  "stream__core"));

builder.Services.AddHostedService<DatabaseBroadcaster>();

var host = builder.Build();
host.Run();
