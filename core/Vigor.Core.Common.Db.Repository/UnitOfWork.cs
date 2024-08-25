using Microsoft.EntityFrameworkCore;

namespace Vigor.Core.Common.Db.Repository;

public class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
  private DbContext DbContext { get; } = dbContext;
  private Dictionary<Type, IRepository> Repositories { get; } = [];

  public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity
  {
    var repositoryType = typeof(TEntity) ?? throw new ArgumentException("Invalid type given.");

    if (!Repositories.TryGetValue(repositoryType, out IRepository? repository))
    {
      repository = new Repository<TEntity>(DbContext);
      Repositories.Add(repositoryType, repository);
    }

    return (IRepository<TEntity>)repository;
  }

  public void Save()
  {
    DbContext.SaveChanges();
  }
}
