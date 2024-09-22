using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
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
  public async Task<IActionResult> Create([FromBody] Domain.Facility.Contracts.UpsertFacility createFacility)
  {
    var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var userId = Guid.Parse(claim);
    var createdFacility = await _facilityCrud.UpsertAsync(userId, createFacility);
    return Ok(createdFacility);
  }

  [Authorize(Policies.IsFacilityOwner)]
  [HttpGet]
  public async Task<IActionResult> FindAsync()
  {
    var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var userId = Guid.Parse(claim);
    var results = await _facilityCrud.FindAsync(userId);
    return Ok(results);
  }

  [Authorize(Policies.IsFacilityOwner)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Remove(Guid id)
  {
    var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var userId = Guid.Parse(claim);
    var removedFacility = await _facilityCrud.RemoveAsync(userId, facilityId: id);
    return Ok(removedFacility);
  }
}
