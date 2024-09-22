using Vigor.Common.Db.Repository;

namespace Vigor.Core.Program.Db.Entities;

public class Program : Entity
{
  public required string Name { get; set; }
  public required decimal MonthlyFee { get; set; }

  #region Relations
  public IEnumerable<Facility> Facilities { get; set; } = [];
  #endregion

  #region External fields
  public required Guid OwnerId { get; set; }
  #endregion
}
