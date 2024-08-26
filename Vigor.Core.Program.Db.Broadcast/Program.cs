using Microsoft.Extensions.Options;

using Vigor.Core.Common.Queue.Redis.Extensions.DependencyInjection;
using Vigor.Core.Program.Db.Broadcast;
using Vigor.Core.Program.Db.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<DbBroadcasterOptions>(builder.Configuration.GetRequiredSection("DbBroadcaster"));

builder.Services.AddProgramDbContextFactory(builder.Configuration.GetConnectionString("ProgramDb"));

builder.Services.AddRedisClient(builder.Configuration.GetConnectionString("Queue"));
builder.Services.AddSingletonRedisStreamPublisher(provider => new()
{
  Stream = provider.GetRequiredService<IOptions<DbBroadcasterOptions>>().Value.Stream
});

builder.Services.AddHostedService<DbBroadcaster>();

var host = builder.Build();
host.Run();
