using System.Reflection;

using Vigor.Common.Auth.Keycloak.Extensions.DependencyInjection;
using Vigor.Common.Db.Repository.Extensions.DependencyInjection;
using Vigor.Core.Program.Db;
using Vigor.Core.Program.Db.Extensions.DependencyInjection;
using Vigor.Core.Program.Domain.Facility.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBasicKeycloakJwtAuthentication(builder.Configuration);

builder.Services.AddProgramDbContext(builder.Configuration.GetConnectionString("ProgramDb"));
builder.Services.AddScopedUnitOfWork(provider => provider.GetRequiredService<ProgramDbContext>());
builder.Services.AddFacilityDomain();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(o => o.IncludeXmlComments(
  Path.Combine(
    AppContext.BaseDirectory,
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")));

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
