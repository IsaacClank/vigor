using Jobs = User.Api.Queue.Jobs;
using User.Api.Queue;
using User.Common.Queue;
using User.Database;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));

var queueConnectionString = builder.Configuration.GetConnectionString("Queue");
var queueConsumerOptions = builder.Configuration.GetSection("Consumer").Get<ConsumerOptions>()
  ?? throw new ArgumentException("Failed to parse QueueConsumer configuration.");

builder.Services.AddSingleton<QueueClient>((_) => new(queueConnectionString));
builder.Services.AddSingleton<IQueueConsumer, QueueConsumer>((provider) => new(
  provider.GetRequiredService<QueueClient>(),
  queueConsumerOptions.Stream,
  queueConsumerOptions.Group,
  queueConsumerOptions.Consumer));
builder.Services.AddHostedService<Jobs.Consumer>(provider => new(
  provider.GetRequiredService<ILogger<Jobs.Consumer>>(),
  provider.GetRequiredService<IQueueConsumer>()));

var host = builder.Build();
host.Run();
