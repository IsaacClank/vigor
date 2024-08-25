using Vigor.Core.User.Api.Http.Dto;

namespace Vigor.Core.User.Api.Http.Services;

public interface IProfileService
{
  /// <summary>
  /// Find a profile by ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns><see cref="Profile"/></returns>
  /// <exception cref="Common.Exception.EntityNotFoundException{Profile}"></exception>
  public Profile Find(int id);

  /// <summary>
  /// Create a new Profile.
  /// </summary>
  /// <param name="data"></param>
  /// <returns><see cref="Profile"/></returns>
  public Profile Create(CreateProfile data);
}
