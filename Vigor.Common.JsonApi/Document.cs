namespace Vigor.Common.JsonApi;

public class Document<T> where T : ContractBase
{
  public IEnumerable<DocumentDataEntry<T>> Data { get; set; } = [];
}
