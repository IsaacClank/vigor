using Microsoft.Extensions.Options;

using Vigor.Core.Common.Queue.Redis.Extensions.DependencyInjection;
using Vigor.Core.User.Db.Broadcast;
using Vigor.Core.User.Db.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<DbBroadcasterOptions>(builder.Configuration.GetRequiredSection("DbBroadcaster"));

builder.Services.AddUserDbContextFactory(builder.Configuration.GetConnectionString("UserDb"));

builder.Services.AddRedisClient(builder.Configuration.GetConnectionString("Queue"));
builder.Services.AddSingletonRedisStreamPublisher(provider => new()
{
  Stream = provider.GetRequiredService<IOptions<DbBroadcasterOptions>>().Value.Stream
});

builder.Services.AddHostedService<DbBroadcaster>();

var host = builder.Build();
host.Run();
