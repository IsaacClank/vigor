using Vigor.Common.Db.Repository;

namespace Vigor.Core.Program.Db.Entities;

public class Membership : Entity
{
  #region Relations
  public Guid ProgramId { get; set; }
  public Program? Program { get; set; }
  #endregion

  #region External
  public required Guid OwnerId { get; set; }
  #endregion
}
