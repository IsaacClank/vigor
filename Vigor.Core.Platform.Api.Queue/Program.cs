using Microsoft.Extensions.Options;

using Vigor.Common.Queue.Redis.Extensions.DependencyInjection;
using Vigor.Core.Platform.Api.Queue;

var builder = Host.CreateApplicationBuilder(args);

#region Options
builder.Services.Configure<QueueConsumerOptions>(builder.Configuration.GetSection("QueueConsumer"));
#endregion

#region Redis clients
builder.Services.AddRedisClient(builder.Configuration.GetConnectionString("Queue"));
builder.Services.AddSingletonRedisStreamConsumer(provider =>
{
  var options = provider.GetRequiredService<IOptions<QueueConsumerOptions>>();

  var queueConsumerOptions = provider.GetRequiredService<IOptions<QueueConsumerOptions>>().Value;
  return new()
  {
    Stream = queueConsumerOptions.Stream,
    Group = queueConsumerOptions.Group,
    Consumer = queueConsumerOptions.Consumer,
  };
});
#endregion

builder.Services.AddHostedService<QueueConsumer>();

var host = builder.Build();
host.Run();
