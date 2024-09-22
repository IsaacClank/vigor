using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
using Vigor.Common.Extensions.System.Security.Claims;
using Vigor.Core.Program.Common.Auth.Keycloak;
using Vigor.Core.Program.Domain.Program.Contracts;
using Vigor.Core.Program.Domain.Program.Interfaces;

namespace Vigor.Core.Program.Api.Http.Controllers;

[Authorize(Policies.IsProgramOwner)]
[ApiController]
[Route("api/program")]
public class ProgramController(IProgramCrud programCrud) : JsonApiController
{
  private IProgramCrud ProgramCrud { get; set; } = programCrud;

  [HttpPost]
  public async Task<IActionResult> UpsertAsync([FromBody] UpsertProgram upsertProgram)
  {
    var upsertedProgram = await ProgramCrud.UpsertAsync(
      HttpContext.User.GetSubjectId(),
      upsertProgram);
    return Ok(upsertedProgram);
  }

  [HttpGet]
  public async Task<IActionResult> FindAsync()
  {
    var results = await ProgramCrud.FindAsync(HttpContext.User.GetSubjectId());
    return Ok(results);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> RemoveAsync([FromRoute] Guid id)
  {
    var deletedProgram = await ProgramCrud.RemoveAsync(HttpContext.User.GetSubjectId(), id);
    return Ok(deletedProgram);
  }
}
