using System.Security.Claims;

namespace Vigor.Common.Extensions.System.Security.Claims;

public static class ClaimsPrincipleExtensions
{
  public static Claim GetRequiredClaim(
    this ClaimsPrincipal principal,
    string claimType)
  {
    return principal.Claims.First(c => c.Type == claimType);
  }

  public static Guid GetSubjectId(this ClaimsPrincipal principal)
    => Guid.Parse(GetRequiredClaim(principal, ClaimTypes.NameIdentifier).Value);
}
