using User.Database;
using User.Jobs;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddUserDbContext(builder.Configuration.GetConnectionString("UserDatabase"));
builder.Services.AddHostedService<DatabaseBroadcaster>();

var host = builder.Build();
host.Run();
