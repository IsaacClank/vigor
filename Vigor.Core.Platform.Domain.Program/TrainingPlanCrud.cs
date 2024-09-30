using Microsoft.Extensions.Logging;

using Vigor.Common.Db.Repository;
using Vigor.Common.Exception;
using Vigor.Common.Extensions.Logging;
using Vigor.Common.Extensions.System;
using Vigor.Common.Util;
using Vigor.Core.Platform.Domain.Program.Contracts;
using Vigor.Core.Platform.Domain.Program.Interfaces;

namespace Vigor.Core.Platform.Domain.Program;

public class TrainingPlanCrud(
  ILogger<TrainingPlanCrud> logger,
  IUnitOfWork unitOfWork) : ITrainingPlanCrud
{
  private ILogger<TrainingPlanCrud> Logger { get; set; } = logger;
  private IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
  private IRepository<Db.Entities.TrainingPlan> TrainingPlanRepo
    => UnitOfWork.Repository<Db.Entities.TrainingPlan>();

  public async Task<IEnumerable<TrainingPlan>> UpsertAsync(
    Guid userId,
    IEnumerable<UpsertTrainingPlan> upsertTrainingPlans)
  {
    List<Db.Entities.TrainingPlan> upsertedTrainingPlans = [];
    await Parallel.ForEachAsync(upsertTrainingPlans, async (upsertTrainingPlan, _) =>
    {
      var trainingPlantoUpsert = Util.Map<Db.Entities.TrainingPlan>(upsertTrainingPlan);
      trainingPlantoUpsert.OwnerId = userId;

      var upsertedTrainingPlan = upsertTrainingPlan.Id.IsEmptyOrDefault()
        ? await TrainingPlanRepo.InsertAsync(trainingPlantoUpsert)
        : TrainingPlanRepo.Update(trainingPlantoUpsert);

      upsertedTrainingPlans.Add(upsertedTrainingPlan);
    });

    await UnitOfWork.SaveAsync();
    upsertedTrainingPlans.ForEach(f => Logger.UpsertedEntity(
      nameof(Db.Entities.TrainingPlan),
      f.Id));

    return Util.MapRange<TrainingPlan>(upsertedTrainingPlans);
  }

  public async Task<IEnumerable<TrainingPlan>> PatchAsync(
    Guid userId,
    IEnumerable<PatchTrainingPlan> patchTrainingPlans)
  {
    var trainingPlanIds = patchTrainingPlans.Select(x => x.Id);
    var trainingPlansDict = (await TrainingPlanRepo
      .FindAsync(f => f.OwnerId == userId && trainingPlanIds.Contains(f.Id)))
      .ToDictionary(f => f.Id, f => f);

    patchTrainingPlans.ToList().ForEach(patchTrainingPlan =>
    {
      var trainingPlan = trainingPlansDict[patchTrainingPlan.Id];
      Util.Map(
        trainingPlan,
        patchTrainingPlan,
        includedProperties:
        [
          nameof(PatchTrainingPlan.Name),
          nameof(PatchTrainingPlan.MonthlyFee),
        ]);
    });
    await UnitOfWork.SaveAsync();

    return Util.MapRange<TrainingPlan>(trainingPlansDict.Values);
  }

  public async Task<IEnumerable<TrainingPlan>> FindAsync(Guid userId)
  {
    var trainingPlans = await TrainingPlanRepo.FindAsync(p => p.OwnerId == userId);
    return Util.MapRange<TrainingPlan>(trainingPlans);
  }

  public async Task<IEnumerable<TrainingPlan>> RemoveAsync(
    Guid userId,
    IEnumerable<Guid> trainingPlanIds)
  {
    var trainingPlans = await TrainingPlanRepo
      .FindAsync(f => f.OwnerId == userId && trainingPlanIds.Contains(f.Id));
    EntityNotFoundException.ThrowIfNull(trainingPlans);

    trainingPlans.ToList().ForEach(f => TrainingPlanRepo.Delete(f));
    await UnitOfWork.SaveAsync();

    return Util.MapRange<TrainingPlan>(trainingPlans);
  }
}
