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
  private readonly IProgramCrud _programCrud = programCrud;

  [HttpPost]
  public async Task<IActionResult> UpsertAsync(
    [FromBody] IEnumerable<UpsertProgram> upsertPrograms)
  {
    return Ok(await _programCrud.UpsertAsync(
      User.GetSubjectId(),
      upsertPrograms));
  }

  [HttpPatch]
  public async Task<IActionResult> PatchAsync(
    [FromBody] IEnumerable<PatchProgram> patchPrograms)
  {
    return Ok(await _programCrud.PatchAsync(
      User.GetSubjectId(),
      patchPrograms));
  }

  [HttpGet]
  public async Task<IActionResult> FindAsync()
  {
    return Ok(await _programCrud.FindAsync(User.GetSubjectId()));
  }

  [HttpDelete]
  public async Task<IActionResult> RemoveAsync(
    [FromBody] IEnumerable<Guid> programIds)
  {
    return Ok(await _programCrud.RemoveAsync(User.GetSubjectId(), programIds));
  }
}
