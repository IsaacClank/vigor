using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;

using JsonApi = Vigor.Common.JsonApi;

namespace Vigor.Core.Program.Api.Http.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class Controller : JsonApiController
{
  [HttpGet]
  [ProducesResponseType<JsonApi.Document<Contracts.Response.Message>>(StatusCodes.Status200OK)]
  public IActionResult Get()
  {
    var x = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
    return Ok<Contracts.Response.Message>(new() { Content = $"Hello {x}" });
  }
}
