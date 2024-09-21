namespace Vigor.Common.Db.Repository;

public interface IUnitOfWork
{
  /// <exception cref="ArgumentException"></exception>
  public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity;

  /// <exception cref="DbUpdateException"/>
  /// <exception cref="DbUpdateConcurrencyException"/>
  public void Save();

  /// <exception cref="DbUpdateException"/>
  /// <exception cref="DbUpdateConcurrencyException"/>
  /// <exception cref="OperationCanceledException"/>
  public Task SaveAsync();
}
