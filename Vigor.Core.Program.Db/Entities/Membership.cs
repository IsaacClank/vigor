using Vigor.Common.Db.Repository;

namespace Vigor.Core.Program.Db.Entities;

public class Membership : Entity
{
  #region Relations
  public int ProgramId { get; set; }
  public Program? Program { get; set; }
  #endregion
}
