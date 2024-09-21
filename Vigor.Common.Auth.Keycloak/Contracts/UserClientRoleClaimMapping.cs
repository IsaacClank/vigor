using System.Text.Json.Serialization;

namespace Vigor.Common.Auth.Keycloak.Contracts;

internal class UserClientRoleClaimMapping
{
  [JsonPropertyName("roles")]
  public IEnumerable<string> Roles { get; set; } = [];
}
