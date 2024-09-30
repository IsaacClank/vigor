namespace Vigor.Core.Platform.Domain.Program.Contracts;

public class PatchTrainingPlan : JsonApi.ContractBase
{
  public new required Guid Id { get; set; }
  public string? Name { get; set; }
  public decimal? MonthlyFee { get; set; }
}
