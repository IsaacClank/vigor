using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Vigor.Common.Db.Repository.Extensions.DependencyInjection;
using Vigor.Core.Platform.Common.Auth.Keycloak.Extensions.DependencyInjection;
using Vigor.Core.Platform.Db;
using Vigor.Core.Platform.Db.Extensions.DependencyInjection;
using Vigor.Core.Platform.Domain.Facility.Extensions.DependencyInjection;
using Vigor.Core.Platform.Domain.Program.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddApiAuthorization();

builder.Services.AddPlatformDbContext(builder.Configuration.GetConnectionString("PlatformDb"));
builder.Services.AddScopedUnitOfWork(provider => provider.GetRequiredService<PlatformDbContext>());
builder.Services.AddFacilityDomain();
builder.Services.AddTrainingPlanDomain();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(o =>
{
  o.SwaggerDoc("v1", new() { Title = "Vigor.Core.Platform.Api.Http", Version = "v1" });
  o.IncludeXmlComments(Path.Combine(
    AppContext.BaseDirectory,
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
  o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
  {
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    In = ParameterLocation.Header,
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
