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

  public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
  {
    TransformResourceAccessClaim(principal);
    return Task.FromResult(principal);
  }

  private static void TransformResourceAccessClaim(ClaimsPrincipal principal)
  {
    if (principal.Identity is not ClaimsIdentity identity)
    {
      return;
    }

    var serializedResourceAccessClaim = identity.FindFirst(ClaimTypes.ResourceAccess);
    if (serializedResourceAccessClaim is null)
    {
      return;
    }

    var resourceAccessClaimValue = JsonSerializer.Deserialize<UserClientRoleClaimValue>(
      serializedResourceAccessClaim.Value);
    if (resourceAccessClaimValue is null)
    {
      return;
    }

    foreach (var role in resourceAccessClaimValue.Roles)
    {
      identity.AddClaim(new(
        System.Security.Claims.ClaimTypes.Role,
        role,
        ClaimValueTypes.String,
        serializedResourceAccessClaim.Issuer,
        serializedResourceAccessClaim.OriginalIssuer,
        serializedResourceAccessClaim.Subject));
    }
  }
}
