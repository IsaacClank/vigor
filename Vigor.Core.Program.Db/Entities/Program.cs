using Vigor.Common.Db.Repository;

namespace Vigor.Core.Program.Db.Entities;

public class Program : Entity
{
  public required string Name { get; set; }
  public decimal MonthlyFee { get; set; } = 0.0M;

  #region Relations
  public IEnumerable<Facility> Facilities { get; set; } = [];
  #endregion

  #region External
  public int OwnerId { get; set; }
  #endregion
}
