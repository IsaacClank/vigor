using Microsoft.AspNetCore.Mvc;

using Vigor.Core.Common.Exception;
using Vigor.Core.Common.Http.Response;
using Vigor.Core.User.Api.Http.Dto;
using Vigor.Core.User.Api.Http.Services;

namespace Vigor.Core.User.Api.Http.Controller;

[Route("api/profile")]
[ApiController]
public class ProfileController(IProfileService profileService) : ControllerBase
{
  protected IProfileService ProfileService { get; } = profileService;

  /// <summary>
  /// Create a new Profile.
  /// </summary>
  ///<returns></returns>
  [HttpPost]
  [ProducesResponseType<ApiResponse<Profile>>(StatusCodes.Status200OK)]
  public IActionResult Create([FromBody] CreateProfile data)
  {
    var response = new ApiResponse<Profile>(ProfileService.Create(data));
    return Ok(response);
  }

  /// <summary>
  /// Get a Profile by its ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id}")]
  [ProducesResponseType<ApiResponse<Profile>>(StatusCodes.Status200OK)]
  [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
  public IActionResult Find([FromRoute] int id)
  {
    try
    {
      var response = new ApiResponse<Profile>(ProfileService.Find(id));
      return Ok(response);
    }
    catch (EntityNotFoundException<Profile> e)
    {
      return NotFound(new ApiErrorResponse(e));
    }
  }
}
