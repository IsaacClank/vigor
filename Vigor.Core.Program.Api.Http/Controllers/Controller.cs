using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;

namespace Vigor.Core.Program.Api.Http.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class Controller : JsonApiController
{
  [HttpGet("health")]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Check()
  {
    var x = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
    return Empty;
  }
}
