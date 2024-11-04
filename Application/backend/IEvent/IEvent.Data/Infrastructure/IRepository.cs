using System.Linq.Expressions;

namespace IEvent.Data.Infrastructure
{
  public interface IRepository<TEntity> where TEntity : class
  {
    void Add(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);

    TEntity GetByKey(params object[] keyValues);

    IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties);

    Task AddAsync(TEntity entity);
  }
}

