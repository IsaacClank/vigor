namespace Vigor.Core.Program.Domain.Program.Contracts;

public class UpsertProgram : JsonApi.ContractBase
{
  public required string Name { get; set; }
  public required decimal MonthlyFee { get; set; }
}
