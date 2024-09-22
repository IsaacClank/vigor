using Vigor.Core.Program.Domain.Program.Contracts;

namespace Vigor.Core.Program.Domain.Program.Interfaces;

public interface IProgramCrud
{
  public Task<Contracts.Program> UpsertAsync(
    Guid userId,
    UpsertProgram upsertProgram);

  public Task<IEnumerable<Contracts.Program>> FindAsync(Guid userId);

  public Task<Contracts.Program> RemoveAsync(Guid userId, Guid programId);
}
