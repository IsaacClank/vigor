namespace Vigor.Core.User.Api.Http.Mapping;

public class ProfileMapping : AutoMapper.Profile
{
  public ProfileMapping()
  {
    CreateMap<Dto.CreateProfile, Db.Model.Profile>();
    CreateMap<Db.Model.Profile, Dto.Profile>();
  }
}
