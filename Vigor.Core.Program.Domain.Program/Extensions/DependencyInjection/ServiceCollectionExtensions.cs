using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Program.Domain.Program.Interfaces;

namespace Vigor.Core.Program.Domain.Program.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddProgramDomain(this IServiceCollection services)
  {
    services.AddScoped<IProgramCrud, ProgramCrud>();
  }
}
