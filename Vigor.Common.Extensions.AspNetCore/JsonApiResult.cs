using Microsoft.AspNetCore.Mvc;

namespace Vigor.Common.Extensions.AspNetCore;

public class JsonApiResult<T> : ObjectResult
  where T : JsonApi.ContractBase
{
  public JsonApiResult(IEnumerable<T> values)
    : base(new JsonApi.Document<T>
    {
      Data = values.Select(value => new JsonApi.DocumentDataEntry<T>(value))
    })
  {
  }
}
