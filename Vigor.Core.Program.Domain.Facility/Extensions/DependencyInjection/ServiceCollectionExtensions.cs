using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Domain.Facility.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddFacilityDomain(this IServiceCollection services)
  {
    services.AddAutoMapper(config =>
    {
      config.CreateMap<Contracts.UpsertFacility, Db.Models.Facility>();
      config.CreateMap<Db.Models.Facility, Contracts.Facility>();
    });

    services.AddScoped<IFacilityCrud, FacilityCrud>();
  }
}
