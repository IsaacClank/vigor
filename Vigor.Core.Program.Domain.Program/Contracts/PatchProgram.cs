namespace Vigor.Core.Program.Domain.Program.Contracts;

public class PatchProgram : JsonApi.ContractBase
{
  public new required Guid Id { get; set; }
  public string? Name { get; set; }
  public decimal? MonthlyFee { get; set; }
}
