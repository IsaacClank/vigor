using AutoMapper;

using Vigor.Core.Common.Db.Repository;
using Vigor.Core.Common.Exception;
using Vigor.Core.User.Api.Http.Dto;

namespace Vigor.Core.User.Api.Http.Services;

public class ProfileService(IUnitOfWork unitOfWork, IMapper mapper) : IProfileService
{
  protected IUnitOfWork UnitOfWork { get; } = unitOfWork;
  protected IMapper Mapper { get; } = mapper;

  /// <inheritdoc/>
  public Dto.Profile Find(int id)
  {
    var profile = UnitOfWork.Repository<Db.Model.Profile>().Find(id)
      ?? throw new EntityNotFoundException<Dto.Profile>() { Source = nameof(id) };
    return Mapper.Map<Dto.Profile>(profile);
  }

  /// <inheritdoc/>
  public Dto.Profile Create(CreateProfile data)
  {
    var profileToInsert = Mapper.Map<Db.Model.Profile>(data);
    var insertedProfile = UnitOfWork.Repository<Db.Model.Profile>().Insert(profileToInsert);
    UnitOfWork.Save();
    return Mapper.Map<Dto.Profile>(insertedProfile);
  }
}
