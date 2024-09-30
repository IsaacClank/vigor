using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Vigor.Common.Auth.Keycloak.Extensions.DependencyInjection;

namespace Vigor.Core.Platform.Common.Auth.Keycloak.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddApiAuthentication(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    services.AddBasicKeycloakJwtAuthentication(configuration);
  }

  public static void AddApiAuthorization(this IServiceCollection services)
  {
    services
        .AddAuthorizationBuilder()
        .AddPolicy(Policies.IsFacilityOwner, p => p.RequireClaim(ClaimTypes.Role, Roles.FacilityOwner))
        .AddPolicy(Policies.IsFitnessEnthusiast, p => p.RequireClaim(ClaimTypes.Role, Roles.FitnessEnthusiast))
        .AddPolicy(Policies.IsTrainingPlanOwner, p => p.RequireClaim(ClaimTypes.Role, Roles.TrainingPlanOwner));
  }
}
