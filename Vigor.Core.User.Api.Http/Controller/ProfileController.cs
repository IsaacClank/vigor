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
  [ProducesResponseType(StatusCodes.Status200OK)]
  public ActionResult<ApiResponse<Profile>> Create([FromBody] CreateProfile data)
  {
    return new ApiResponse<Profile>(ProfileService.Create(data));
  }

  /// <summary>
  /// Get a Profile by its ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType<ApiResponse>(StatusCodes.Status404NotFound)]
  public ActionResult<ApiResponse<Profile>> Find([FromRoute] int id)
  {
    try
    {
      return new ApiResponse<Profile>(ProfileService.Find(id));
    }
    catch (EntityNotFoundException<Profile> e)
    {
      return NotFound(new ApiResponse(e));
    }
  }
}
