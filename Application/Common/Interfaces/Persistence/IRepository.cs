using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(string id);
    Task<IEnumerable<TEntity>> ListAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpsertAsync(TEntity entity);
    Task RemoveAsync(string id);
}