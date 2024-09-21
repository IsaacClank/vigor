
using Microsoft.EntityFrameworkCore;

namespace Vigor.Common.Db.Repository;

public class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
  private DbContext DbContext { get; } = dbContext;
  private Dictionary<Type, IRepository<Entity>> Repositories { get; } = [];

  public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity
  {
    var repositoryType = typeof(TEntity)
      ?? throw new ArgumentException("Invalid type given.");

    if (!Repositories.TryGetValue(repositoryType, out IRepository<Entity>? repository)
      || repository == null)
    {
      repository = (IRepository<Entity>)new Repository<TEntity>(DbContext);
      Repositories.Add(repositoryType, repository);
    }

    return (IRepository<TEntity>)repository;
  }

  public void Save() => DbContext.SaveChanges();
  public Task SaveAsync() => DbContext.SaveChangesAsync();
}
