using Microsoft.Extensions.DependencyInjection;

using Vigor.Core.Program.Domain.Program.Interfaces;

namespace Vigor.Core.Program.Domain.Program.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static void AddProgramDomain(this IServiceCollection services)
  {
    services.AddAutoMapper(config =>
    {
      config.CreateMap<Contracts.UpsertProgram, Db.Entities.Program>();
      config.CreateMap<Db.Entities.Program, Contracts.Program>();
    });

    services.AddScoped<IProgramCrud, ProgramCrud>();
  }
}