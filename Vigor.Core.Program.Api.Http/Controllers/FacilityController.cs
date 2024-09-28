using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
using Vigor.Common.Extensions.System.Security.Claims;
using Vigor.Common.JsonApi;
using Vigor.Core.Program.Common.Auth.Keycloak;
using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Api.Http.Controllers;

[ApiController]
[Route("api/facility")]
public class FacilityController(IFacilityCrud facilityCrud) : JsonApiController
{
  private readonly IFacilityCrud _facilityCrud = facilityCrud;

  [Authorize(Policies.IsFacilityOwner)]
  [HttpPost]
  [ProducesResponseType<Document<Domain.Facility.Contracts.Facility>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> Create(
    [FromBody] Domain.Facility.Contracts.UpsertFacility createFacility)
  {
    var createdFacility = await _facilityCrud.UpsertAsync(
      User.GetSubjectId(),
      createFacility);
    return Ok(createdFacility);
  }

  [Authorize(Policies.IsFacilityOwner)]
  [HttpPatch("merge")]
  [ProducesResponseType<Document<Domain.Facility.Contracts.Facility>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> MergeUpdate(
    [FromBody] IEnumerable<Domain.Facility.Contracts.PatchFacility> mergeUpdateFacilities)
  {
    var updatedFacilities = await _facilityCrud.PatchAsync(
      User.GetSubjectId(),
      mergeUpdateFacilities);
    return Ok(updatedFacilities);
  }

  [Authorize(Policies.IsFacilityOwner)]
  [HttpGet]
  public async Task<IActionResult> FindAsync()
  {
    return Ok(await _facilityCrud.FindAsync(User.GetSubjectId()));
  }

  [Authorize(Policies.IsFacilityOwner)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Remove(Guid id)
  {
    return Ok(await _facilityCrud.RemoveAsync(
      User.GetSubjectId(),
      facilityId: id));
  }
}
