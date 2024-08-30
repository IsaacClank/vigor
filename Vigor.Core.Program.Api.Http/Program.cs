using Vigor.Core.Common.Db.Repository.Extensions.DependencyInjection;
using Vigor.Core.Program.Db.Extensions.DependencyInjection;
using System.Reflection;
using Vigor.Core.Program.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDbContext(builder.Configuration.GetConnectionString("ProgramDb"));
builder.Services.AddScopedUnitOfWork(provider => provider.GetRequiredService<ProgramDbContext>());
builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(o => o.IncludeXmlComments(
  Path.Combine(
      AppContext.BaseDirectory,
      $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
