using Microsoft.AspNetCore.Mvc;

namespace Vigor.Common.Extensions.AspNetCore;

public class JsonApiResult<T>(T value) : ObjectResult(new JsonApi.Document<T>(value))
  where T : JsonApi.ContractBase
{
}
