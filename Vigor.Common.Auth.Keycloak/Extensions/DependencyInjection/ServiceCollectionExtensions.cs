using System.Text.Json;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Vigor.Common.Auth.Keycloak.Contracts;
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
    ArgumentException.ThrowIfNullOrWhiteSpace(keycloakOptions?.ServerUrl);
    ArgumentException.ThrowIfNullOrWhiteSpace(keycloakOptions?.Realm);

    Console.WriteLine(JsonSerializer.Serialize(keycloakOptions));

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
        ValidIssuers = keycloakOptions.ValidIssuers,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,
        IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
        {
          HttpClient httpClient = new()
          {
            BaseAddress = new Uri(keycloakOptions.ServerUrl)
          };
          var request = new HttpRequestMessage(HttpMethod.Get, $"realms/{keycloakOptions.Realm}/protocol/openid-connect/certs");
          var response = httpClient.Send(request);
          using var contentReader = new StreamReader(response.Content.ReadAsStream());
          var realmCerts = JsonSerializer.Deserialize<RealmCerts>(contentReader.ReadToEnd());
          var keys = realmCerts?.Keys.Select(k => JsonWebKey.Create(JsonSerializer.Serialize(k))) ?? [];
          return keys.Where(k => k.KeyId == kid);
        }
      };
    });
  }

  public static void AddKeycloakAuthorization(this IServiceCollection services)
  {
    services.AddAuthorization();
  }
}
