namespace Vigor.Core.Common.Db.Repository;

public interface IUnitOfWork
{
  public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
  public void Save();
  public Task SaveAsync();
}
