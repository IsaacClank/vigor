using System.Linq.Expressions;

namespace Vigor.Core.Common.Db.Repository;

public interface IRepository
{ }

public interface IRepository<TEntity> : IRepository where TEntity : Entity
{
  /// <summary>
  /// Find an entity instance by Id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public TEntity? Find(int id);

  /// <summary>
  /// </summary>
  /// <param name="predicate"></param>
  /// <param name="limit"></param>
  /// <param name="offset"></param>
  /// <returns>An unordered list of entities that fullfil <paramref name="predicate"/>.</returns>
  public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int limit = int.MaxValue, int offset = 0);

  /// <summary>
  /// </summary>
  /// <param name="predicate"></param>
  /// <param name="orderBy"></param>
  /// <param name="limit"></param>
  /// <param name="offset"></param>
  /// <returns>An ordered list of entities that fullfil <paramref name="predicate"/>.</returns>
  public IEnumerable<TEntity> Find<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderBy, int limit = int.MaxValue, int offset = 0);

  /// <summary>
  /// </summary>
  /// <param name="predicate"></param>
  /// <param name="limit"></param>
  /// <param name="offset"></param>
  /// <returns>An unordered list of entities that fullfil <paramref name="predicate"/>.</returns>
  public IEnumerable<TEntity> FindReadonly(Expression<Func<TEntity, bool>> predicate, int limit = int.MaxValue, int offset = 0);

  /// <summary>
  /// </summary>
  /// <param name="predicate"></param>
  /// <param name="orderBy"></param>
  /// <param name="limit"></param>
  /// <param name="offset"></param>
  /// <returns>An ordered list of entities that fullfil <paramref name="predicate"/>.</returns>
  public IEnumerable<TEntity> FindReadonly<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderBy, int limit = int.MaxValue, int offset = 0);

  /// <summary>
  /// </summary>
  /// <param name="entity"></param>
  public TEntity Insert(TEntity entity);

  /// <summary>
  /// </summary>
  /// <param name="entity"></param>
  public TEntity Update(TEntity entity);

  /// <summary>
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public TEntity Delete(TEntity entity);
}
