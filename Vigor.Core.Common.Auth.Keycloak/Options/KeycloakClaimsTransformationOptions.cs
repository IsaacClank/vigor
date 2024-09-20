namespace Vigor.Core.Common.Auth.Keycloak.Options;

public class KeycloakClaimsTransformationOptions
{
  public const string Section = "KeycloakClaimsTransformation";

  public bool ShouldTransformUserClientRoleClaim { get; set; } = true;
  public string UserClientRoleClaim { get; set; } = "resource_access";
}
