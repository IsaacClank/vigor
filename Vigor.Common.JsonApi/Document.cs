namespace Vigor.Common.JsonApi;

public class Document<T>(T contract) where T : ContractBase
{
  public DocumentData<T> Data { get; set; } = new(contract);
}
