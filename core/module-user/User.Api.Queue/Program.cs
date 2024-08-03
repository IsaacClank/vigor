using User.Api.Queue;
using User.Api.Queue.Configuration;
using User.Common.Queue;
using User.Database;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddSingleton<QueueClient>((_) => new(builder.Configuration.GetConnectionString("Queue")));

var consumerConfig = builder.Configuration.GetSection("QueueConsumer").Get<QueueConsumerConfiguration>()
  ?? throw new ArgumentException("Failed to parse QueueConsumer configuration.");

builder.Services.AddSingleton<IQueueConsumer, QueueConsumer>((provider) => new(
  provider.GetRequiredService<QueueClient>(),
  consumerConfig.Stream,
  consumerConfig.Group,
  consumerConfig.Consumer));

builder.Services.AddHostedService<Consumer>(provider => new(
  provider.GetRequiredService<ILogger<Consumer>>(),
  provider.GetRequiredService<IQueueConsumer>()));

var host = builder.Build();
host.Run();
