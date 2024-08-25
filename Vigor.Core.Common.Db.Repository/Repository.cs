using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Vigor.Core.Common.Db.Repository;

public class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : Entity
{
  protected DbContext DbContext { get; } = dbContext;
  protected DbSet<TEntity> Model { get; } = dbContext.Set<TEntity>();

  /// <inheritdoc/>
  public TEntity? Find(int id) => Model.Find(id);

  /// <inheritdoc/>
  public IEnumerable<TEntity> Find(
    Expression<Func<TEntity, bool>> predicate,
    int limit = int.MaxValue,
    int offset = 0) => Model.Where(predicate).Take(limit).Skip(offset).ToList();

  /// <inheritdoc/>
  public IEnumerable<TEntity> Find<TOrderKey>(
    Expression<Func<TEntity, bool>> predicate,
    Expression<Func<TEntity, TOrderKey>> orderBy,
    int limit = int.MaxValue,
    int offset = 0) => Model.OrderBy(orderBy).Where(predicate).Take(limit).Skip(offset).ToList();

  /// <inheritdoc/>
  public IEnumerable<TEntity> FindReadonly(
    Expression<Func<TEntity, bool>> predicate,
    int limit = int.MaxValue,
    int offset = 0) => Model.Where(predicate).Take(limit).Skip(offset).AsNoTracking().ToList();

  /// <inheritdoc/>
  public IEnumerable<TEntity> FindReadonly<TOrderKey>(
    Expression<Func<TEntity, bool>> predicate,
    Expression<Func<TEntity, TOrderKey>> orderBy,
    int limit = int.MaxValue,
    int offset = 0) => Model.OrderBy(orderBy).Where(predicate).Take(limit).Skip(offset).AsNoTracking().ToList();

  /// <inheritdoc/>
  public TEntity Insert(TEntity entity)
  {
    entity.CreatedAt = entity.UpdatedAt = DateTime.UtcNow;
    return Model.Add(entity).Entity;
  }

  /// <inheritdoc/>
  public TEntity Update(TEntity entity)
  {
    entity.UpdatedAt = DateTime.UtcNow;
    return Model.Update(entity).Entity;
  }

  /// <inheritdoc/>
  public TEntity Delete(TEntity entity) => Model.Remove(entity).Entity;
}
