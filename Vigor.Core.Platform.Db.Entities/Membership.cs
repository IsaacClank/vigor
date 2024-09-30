using Vigor.Common.Db.Repository;

namespace Vigor.Core.Platform.Db.Entities;

public class Membership : Entity
{
  #region Relations
  public Guid TrainingPlanId { get; set; }
  public TrainingPlan? TrainingPlan { get; set; }
  #endregion

  #region External
  public required Guid OwnerId { get; set; }
  #endregion
}
