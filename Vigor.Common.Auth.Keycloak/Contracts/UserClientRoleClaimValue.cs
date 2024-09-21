namespace Vigor.Common.Auth.Keycloak.Contracts;

internal class UserClientRoleClaimValue : Dictionary<string, UserClientRoleClaimMapping>
{
  public IEnumerable<string> Roles
  {
    get => Values.SelectMany(mapping => mapping.Roles);
  }
}
