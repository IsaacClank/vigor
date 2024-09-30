using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;

namespace Vigor.Core.Platform.Api.Http.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class Controller : JsonApiController
{
  [HttpGet]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Me()
  {
    var nickname = HttpContext.User.Claims.FirstOrDefault(
      c => c.Type == Keycloak.ClaimTypes.NickName)?.Value ?? string.Empty;
    return Content($"Hello {nickname}");
  }

  [HttpGet("health")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public IActionResult Check() => Empty;
}
