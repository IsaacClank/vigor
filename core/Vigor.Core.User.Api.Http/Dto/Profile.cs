using System.ComponentModel.DataAnnotations;

namespace Vigor.Core.User.Api.Http.Dto;

public class Profile
{
  [Required]
  public int Id { get; set; }

  [Required]
  public ProfileType Type { get; set; }
}
