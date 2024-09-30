using Vigor.Core.Platform.Domain.Program.Contracts;

namespace Vigor.Core.Platform.Domain.Program.Interfaces;

public interface ITrainingPlanCrud
{
  public Task<IEnumerable<TrainingPlan>> UpsertAsync(
    Guid userId,
    IEnumerable<UpsertTrainingPlan> upsertTrainingPlans);

  public Task<IEnumerable<TrainingPlan>> PatchAsync(
    Guid userId,
    IEnumerable<PatchTrainingPlan> patchTrainingPlans);

  public Task<IEnumerable<TrainingPlan>> FindAsync(Guid userId);

  public Task<IEnumerable<TrainingPlan>> RemoveAsync(
    Guid userId,
    IEnumerable<Guid> trainingPlanIds);
}
