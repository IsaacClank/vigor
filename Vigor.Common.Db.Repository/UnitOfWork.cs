
using Microsoft.EntityFrameworkCore;

namespace Vigor.Common.Db.Repository;

public class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
  private DbContext DbContext { get; } = dbContext;
  private Dictionary<Type, IRepository> Repositories { get; } = [];

  /// <inheritdoc/>
  public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity
  {
    var repositoryType = typeof(TEntity)
      ?? throw new ArgumentException("Invalid type given.", nameof(TEntity));

    if (!Repositories.TryGetValue(repositoryType, out IRepository? repository)
      || repository == null)
    {
      repository = new Repository<TEntity>(DbContext);
      Repositories.Add(repositoryType, repository);
    }

    return (IRepository<TEntity>)repository;
  }

  /// <inheritdoc/>
  public void Save() => DbContext.SaveChanges();

  /// <inheritdoc/>
  public Task SaveAsync() => DbContext.SaveChangesAsync();
}
