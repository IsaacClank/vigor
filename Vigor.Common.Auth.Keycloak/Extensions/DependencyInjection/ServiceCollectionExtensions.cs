using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Vigor.Common.Auth.Keycloak.Options;

namespace Vigor.Common.Auth.Keycloak.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Add JWT bearer authentication scheme that performs basic verification against a Keycloak server.
  /// </summary>
  public static void AddBasicKeycloakJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    var keycloakOptions = configuration.GetRequiredSection(KeycloakOptions.Section).Get<KeycloakOptions>();
    ArgumentException.ThrowIfNullOrWhiteSpace(keycloakOptions?.Authority);
    ArgumentException.ThrowIfNullOrWhiteSpace(keycloakOptions?.Audience);

    services
      .AddOptions<KeycloakOptions>()
      .Bind(configuration.GetSection(KeycloakOptions.Section))
      .ValidateDataAnnotations();
    services.AddScoped<IClaimsTransformation, KeycloakClaimsTransformation>();
    services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
      o.Authority = keycloakOptions.Authority;
      o.Audience = keycloakOptions.Audience;
      o.RequireHttpsMetadata = keycloakOptions.RequireHttpsMetadata;
      o.TokenValidationParameters = new()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,
      };
    });
  }

  public static void AddKeycloakAuthorization(this IServiceCollection services)
  {
    services.AddAuthorization();
  }
}
