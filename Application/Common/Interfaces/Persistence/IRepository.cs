using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> GetAsync(string id);
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken token = default);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
    Task AddAsync(TEntity entity, CancellationToken token = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token = default);
    Task UpsertAsync(TEntity entity, CancellationToken token = default);
    Task RemoveAsync(string id);
}