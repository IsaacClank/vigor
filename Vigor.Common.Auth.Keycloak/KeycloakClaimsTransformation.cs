using System.Security.Claims;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

using Vigor.Common.Auth.Keycloak.Contracts;
using Vigor.Common.Auth.Keycloak.Options;

namespace Vigor.Common.Auth.Keycloak;

public class KeycloakClaimsTransformation(
  IOptions<KeycloakOptions> options) : IClaimsTransformation
{
  protected IOptions<KeycloakOptions> Options { get; } = options;
  protected KeycloakClaimsTransformationOptions TransformationOptions => Options.Value.ClaimsTransformation;

  public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
  {
    TransformUserClientRoleClaim(principal);
    return Task.FromResult(principal);
  }

  private void TransformUserClientRoleClaim(ClaimsPrincipal principal)
  {
    if (!TransformationOptions.ShouldTransformUserClientRoleClaim
      || principal.Identity is not ClaimsIdentity identity)
    {
      return;
    }

    var rawUserClientRoleClaim = identity.FindFirst(TransformationOptions.UserClientRoleClaim);
    if (rawUserClientRoleClaim is null)
    {
      return;
    }

    var userClientRoleClaim = JsonSerializer.Deserialize<UserClientRoleClaimValue>(rawUserClientRoleClaim.Value);
    if (userClientRoleClaim is null)
    {
      return;
    }

    foreach (var role in userClientRoleClaim.Roles)
    {
      identity.AddClaim(new(
        ClaimTypes.Role,
        role,
        ClaimValueTypes.String,
        rawUserClientRoleClaim.Issuer,
        rawUserClientRoleClaim.OriginalIssuer,
        rawUserClientRoleClaim.Subject));
    }
  }
}
