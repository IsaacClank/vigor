using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
using Vigor.Common.Extensions.System.Security.Claims;
using Vigor.Core.Platform.Common.Auth.Keycloak;
using Vigor.Core.Platform.Domain.Facility.Interfaces;

namespace Vigor.Core.Platform.Api.Http.Controllers;

[Authorize(Policy = Policies.IsFacilityOwner)]
[ApiController]
[Route("api/facility")]
public class FacilityController(IFacilityCrud facilityCrud) : JsonApiController
{
  private readonly IFacilityCrud _facilityCrud = facilityCrud;

  [HttpPost]
  public async Task<ActionResult<JsonApi.Document<Facility.Contracts.Facility>>> Create(
    [FromBody] IEnumerable<Facility.Contracts.UpsertFacility> createFacilities)
  {
    return Ok(await _facilityCrud.UpsertAsync(
      User.GetSubjectId(),
      createFacilities));
  }

  [HttpPatch]
  public async Task<ActionResult<JsonApi.Document<Facility.Contracts.Facility>>> MergeUpdate(
    [FromBody] IEnumerable<Facility.Contracts.PatchFacility> mergeUpdateFacilities)
  {
    return Ok(await _facilityCrud.PatchAsync(
      User.GetSubjectId(),
      mergeUpdateFacilities));
  }

  [HttpGet]
  public async Task<ActionResult<JsonApi.Document<Facility.Contracts.Facility>>> FindAsync()
  {
    return Ok(await _facilityCrud.FindAsync(User.GetSubjectId()));
  }

  [HttpDelete]
  public async Task<ActionResult<JsonApi.Document<Facility.Contracts.Facility>>> Remove(
    [FromBody] IEnumerable<Guid> ids)
  {
    return Ok(await _facilityCrud.RemoveAsync(
      User.GetSubjectId(),
      ids));
  }
}
