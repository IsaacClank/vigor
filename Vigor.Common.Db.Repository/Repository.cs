using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Vigor.Common.Db.Repository;

public class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : Entity
{
  protected DbContext DbContext { get; } = dbContext;
  protected DbSet<TEntity> Model { get; } = dbContext.Set<TEntity>();

  /// <inheritdoc/>
  public TEntity? Find(params object[] id) => Model.Find(id);

  /// <inheritdoc/>
  public async Task<TEntity?> FindAsync(params object[] id) => await Model.FindAsync(id);

  /// <inheritdoc/>
  public IEnumerable<TEntity> Find(
    Expression<Func<TEntity, bool>> predicate,
    int limit = int.MaxValue,
    int offset = 0) => Model.Where(predicate).OrderBy(e => e.CreatedAt).Take(limit).Skip(offset).ToList();

  /// <inheritdoc/>
  public async Task<IEnumerable<TEntity>> FindAsync(
    Expression<Func<TEntity, bool>> predicate,
    int limit = int.MaxValue,
    int offset = 0) => await Model.Where(predicate).OrderBy(e => e.CreatedAt).Take(limit).Skip(offset).ToListAsync();

  /// <inheritdoc/>
  public IEnumerable<TEntity> Find<TOrderKey>(
    Expression<Func<TEntity, bool>> predicate,
    Expression<Func<TEntity, TOrderKey>> orderBy,
    int limit = int.MaxValue,
    int offset = 0) => Model.OrderBy(orderBy).Where(predicate).Take(limit).Skip(offset).ToList();

  /// <inheritdoc/>
  public async Task<IEnumerable<TEntity>> FindAsync<TOrderKey>(
    Expression<Func<TEntity, bool>> predicate,
    Expression<Func<TEntity, TOrderKey>> orderBy,
    int limit = int.MaxValue,
    int offset = 0) => await Model.OrderBy(orderBy).Where(predicate).Take(limit).Skip(offset).ToListAsync();

  /// <inheritdoc/>
  public IEnumerable<TEntity> FindReadonly(
    Expression<Func<TEntity, bool>> predicate,
    int limit = int.MaxValue,
    int offset = 0) => Model.Where(predicate).Take(limit).Skip(offset).AsNoTracking().ToList();

  /// <inheritdoc/>
  public async Task<IEnumerable<TEntity>> FindReadonlyAsync(
    Expression<Func<TEntity, bool>> predicate,
    int limit = int.MaxValue,
    int offset = 0) => await Model.Where(predicate).Take(limit).Skip(offset).AsNoTracking().ToListAsync();

  /// <inheritdoc/>
  public IEnumerable<TEntity> FindReadonly<TOrderKey>(
    Expression<Func<TEntity, bool>> predicate,
    Expression<Func<TEntity, TOrderKey>> orderBy,
    int limit = int.MaxValue,
    int offset = 0) => Model.OrderBy(orderBy).Where(predicate).Take(limit).Skip(offset).AsNoTracking().ToList();

  /// <inheritdoc/>
  public async Task<IEnumerable<TEntity>> FindReadonlyAsync<TOrderKey>(
    Expression<Func<TEntity, bool>> predicate,
    Expression<Func<TEntity, TOrderKey>> orderBy,
    int limit = int.MaxValue,
    int offset = 0) => await Model.OrderBy(orderBy).Where(predicate).Take(limit).Skip(offset).AsNoTracking().ToListAsync();

  /// <inheritdoc/>
  public TEntity Insert(TEntity entity)
  {
    entity.CreatedAt = entity.UpdatedAt = DateTime.UtcNow;
    return Model.Add(entity).Entity;
  }

  /// <inheritdoc/>
  public async Task<TEntity> InsertAsync(TEntity entity)
  {
    entity.CreatedAt = entity.UpdatedAt = DateTime.UtcNow;
    return (await Model.AddAsync(entity)).Entity;
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
