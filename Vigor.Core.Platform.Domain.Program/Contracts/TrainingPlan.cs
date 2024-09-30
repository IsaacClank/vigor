namespace Vigor.Core.Platform.Domain.Program.Contracts;

public class TrainingPlan : JsonApi.ContractBase
{
  public required string Name { get; set; }
  public required decimal MonthlyFee { get; set; }
}
