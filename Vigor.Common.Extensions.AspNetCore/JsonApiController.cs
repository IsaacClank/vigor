using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vigor.Common.Extensions.AspNetCore;

public class JsonApiController : ControllerBase
{
  public virtual JsonApiResult<T> Ok<T>(T value) where T : JsonApi.ContractBase
  {
    return new JsonApiResult<T>(value)
    {
      ContentTypes = ["application/vnd.api+json"],
      StatusCode = StatusCodes.Status200OK,
    };
  }
}
