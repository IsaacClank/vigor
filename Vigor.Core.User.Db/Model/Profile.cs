using Vigor.Core.Common.Db.Repository;

namespace Vigor.Core.User.Db.Model;

public class Profile : Entity
{
  public ProfileType Type { get; set; }
}
