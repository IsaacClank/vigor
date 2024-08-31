using Microsoft.AspNetCore.Mvc;

using Vigor.Core.Common.Http.Response;

namespace Vigor.Core.Program.Api.Http.Controllers;

[Route("api")]
[ApiController]
public class Controller : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<ApiResponse<Dictionary<string, string>>>(StatusCodes.Status200OK)]
  public IActionResult Get()
  {
    Dictionary<string, string> response = new() { { "message", "Hello" } };
    return Ok(new ApiResponse<Dictionary<string, string>>(response));
  }
}
