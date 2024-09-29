using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
using Vigor.Common.Extensions.System.Security.Claims;
using Vigor.Core.Program.Common.Auth.Keycloak;
using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Api.Http.Controllers;

[Authorize(Policy = Policies.IsFacilityOwner)]
[ApiController]
[Route("api/facility")]
public class FacilityController(IFacilityCrud facilityCrud) : JsonApiController
{
  private readonly IFacilityCrud _facilityCrud = facilityCrud;

  [HttpPost]
  public async Task<ActionResult<JsonApi.Document<FacilityContracts.Facility>>> Create(
    [FromBody] IEnumerable<FacilityContracts.UpsertFacility> createFacilities)
  {
    return Ok(await _facilityCrud.UpsertAsync(
      User.GetSubjectId(),
      createFacilities));
  }

  [HttpPatch]
  public async Task<ActionResult<JsonApi.Document<FacilityContracts.Facility>>> MergeUpdate(
    [FromBody] IEnumerable<FacilityContracts.PatchFacility> mergeUpdateFacilities)
  {
    return Ok(await _facilityCrud.PatchAsync(
      User.GetSubjectId(),
      mergeUpdateFacilities));
  }

  [HttpGet]
  public async Task<ActionResult<JsonApi.Document<FacilityContracts.Facility>>> FindAsync()
  {
    return Ok(await _facilityCrud.FindAsync(User.GetSubjectId()));
  }

  [HttpDelete]
  public async Task<ActionResult<JsonApi.Document<FacilityContracts.Facility>>> Remove(
    [FromBody] IEnumerable<Guid> ids)
  {
    return Ok(await _facilityCrud.RemoveAsync(
      User.GetSubjectId(),
      ids));
  }
}
