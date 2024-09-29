using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Vigor.Common.Db.Repository.Extensions.DependencyInjection;
using Vigor.Core.Program.Common.Auth.Keycloak.Extensions.DependencyInjection;
using Vigor.Core.Program.Db;
using Vigor.Core.Program.Db.Extensions.DependencyInjection;
using Vigor.Core.Program.Domain.Facility.Extensions.DependencyInjection;
using Vigor.Core.Program.Domain.Program.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddApiAuthorization();

builder.Services.AddProgramDbContext(builder.Configuration.GetConnectionString("ProgramDb"));
builder.Services.AddScopedUnitOfWork(provider => provider.GetRequiredService<ProgramDbContext>());
builder.Services.AddFacilityDomain();
builder.Services.AddProgramDomain();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(o =>
{
  o.SwaggerDoc("v1", new() { Title = "Vigor.Core.Program.Api.Http", Version = "v1" });
  o.IncludeXmlComments(Path.Combine(
    AppContext.BaseDirectory,
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
  o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
  {
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    In = ParameterLocation.Header,
  });
  o.AddSecurityDefinition("OAuth2.0", new()
  {
    Type = SecuritySchemeType.OpenIdConnect,
    OpenIdConnectUrl = new("http://localhost:2000/realms/vigor-dev/.well-known/openid-configuration"),
  });
  o.AddSecurityRequirement(new()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = JwtBearerDefaults.AuthenticationScheme
        }
      },
      []
    }
  });
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
