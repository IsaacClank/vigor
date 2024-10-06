using System.Text.Json.Serialization;

namespace Vigor.Common.Auth.Keycloak.Contracts;

public class RealmCerts
{
  [JsonPropertyName("keys")]
  public IEnumerable<object> Keys { get; set; } = [];
}
