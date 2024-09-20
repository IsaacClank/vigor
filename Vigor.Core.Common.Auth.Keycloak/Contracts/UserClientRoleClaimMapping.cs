using System.Text.Json.Serialization;

namespace Vigor.Core.Common.Auth.Keycloak.Contracts;

internal class UserClientRoleClaimMapping
{
  [JsonPropertyName("roles")]
  public IEnumerable<string> Roles { get; set; } = [];
}
