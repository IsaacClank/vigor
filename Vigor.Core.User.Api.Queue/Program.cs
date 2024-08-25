using Microsoft.Extensions.Options;

using Vigor.Core.Common.Queue.Redis.Extensions.DependencyInjection;
using Vigor.Core.User.Api.Queue;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<QueueConsumerOptions>(builder.Configuration.GetSection("QueueConsumer"));

builder.Services.AddRedisClient(builder.Configuration.GetConnectionString("Queue"));
builder.Services.AddSingletonRedisStreamConsumer(provider =>
{
  var queueConsumerOptions = provider.GetRequiredService<IOptions<QueueConsumerOptions>>().Value;
  return new()
  {
    Stream = queueConsumerOptions.Stream,
    Group = queueConsumerOptions.Group,
    Consumer = queueConsumerOptions.Consumer,
  };
});

builder.Services.AddHostedService<QueueConsumer>();

var host = builder.Build();
host.Run();
