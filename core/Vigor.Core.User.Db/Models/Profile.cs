using Microsoft.EntityFrameworkCore;

namespace Vigor.Core.User.Db.Models;

public enum ProfileType
{
  FitnessEthusiast,
  ProgramOwner,
}

[Index(nameof(Type), IsUnique = true)]
public class Profile : BaseModel
{
  public ProfileType Type { get; set; }
}
