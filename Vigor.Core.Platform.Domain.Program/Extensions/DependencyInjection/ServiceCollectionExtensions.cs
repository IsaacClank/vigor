using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Platform.Domain.Program.Interfaces;

namespace Vigor.Core.Platform.Domain.Program.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddTrainingPlanDomain(this IServiceCollection services)
  {
    services.AddScoped<ITrainingPlanCrud, TrainingPlanCrud>();
  }
}
