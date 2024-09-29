using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Program.Domain.Facility.Interfaces;

namespace Vigor.Core.Program.Domain.Facility.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddFacilityDomain(this IServiceCollection services)
  {
    services.AddScoped<IFacilityCrud, FacilityCrud>();
  }
}
