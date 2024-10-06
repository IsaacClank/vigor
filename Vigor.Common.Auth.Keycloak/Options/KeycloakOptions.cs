using System.ComponentModel.DataAnnotations;

namespace Vigor.Common.Auth.Keycloak.Options;

public class KeycloakOptions
{
  public const string Section = "Keycloak";

  [Required(AllowEmptyStrings = false)]
  public required string ServerUrl { get; set; }

  [Required(AllowEmptyStrings = false)]
  public required string Realm { get; set; }

  [Required(AllowEmptyStrings = false)]
  public required string Authority { get; set; }

  [Required(AllowEmptyStrings = false)]
  public required string Audience { get; set; }

  public bool RequireHttpsMetadata { get; set; } = true;

  public string[] ValidIssuers { get; set; } = [];
}
