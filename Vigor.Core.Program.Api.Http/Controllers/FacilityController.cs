using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
using Vigor.Common.JsonApi;
using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Api.Http.Controllers;

[ApiController]
[Route("api/facility")]
public class FacilityController(IFacilityCrud facilityCrud) : JsonApiController
{
  private readonly IFacilityCrud _facilityCrud = facilityCrud;

  [Authorize]
  [HttpGet("check")]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Greeting()
  {
    var givenNameClaim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.GivenName);
    return Content($"Hello, {givenNameClaim.Value}!");
  }

  [Authorize(Roles = "program-owner")]
  [HttpPost]
  [ProducesResponseType<Document<Domain.Facility.Contracts.Facility>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> Create([FromBody] Domain.Facility.Contracts.UpsertFacility createFacility)
  {
    var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var userId = Guid.Parse(claim);
    var createdFacility = await _facilityCrud.UpsertAsync(userId, createFacility);
    return Ok(createdFacility);
  }

  [Authorize(Roles = "program-owner")]
  [HttpGet]
  public async Task<IActionResult> FindAsync()
  {
    var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var userId = Guid.Parse(claim);
    var results = await _facilityCrud.FindAsync(userId);
    return Ok(results);
  }

  [Authorize(Roles = "program-owner")]
  [HttpDelete("{facility-id}")]
  public async Task<IActionResult> Remove([FromRoute(Name = "facility-id")] Guid facilityId)
  {
    var claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var userId = Guid.Parse(claim);
    var removedFacility = await _facilityCrud.RemoveAsync(userId, facilityId);
    return Ok(removedFacility);
  }
}
