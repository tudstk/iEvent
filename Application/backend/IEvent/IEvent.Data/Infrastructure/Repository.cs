using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IEvent.Data.Infrastructure
{
  public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
  {
    private readonly IEventContext plannerContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(IEventContext dataContext)
    {
      this.plannerContext = dataContext;
      dbSet = dataContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
      dbSet.Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
      await dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
      await dbSet.AddRangeAsync(entities);
    }

    public virtual void Update(TEntity entity)
    {
      dbSet.Attach(entity);
      plannerContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
      dbSet.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
      dbSet.RemoveRange(entities);
    }

    public virtual TEntity GetByKey(params object[] keyValues)
    {
      return dbSet.Find(keyValues);
    }

    public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties)
    {
      IQueryable<TEntity> query = dbSet;

      foreach (var includeProperty in includeProperties)
      {
        query = query.Include(includeProperty);
      }
      return query;
    }
  }
}
