using System.Linq.Expressions;
using Application.Common.Interfaces.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected DbContext Context;
    internal readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken token = default)
    {
        await DbSet.AddAsync(entity, token);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
    {
        await DbSet.AddRangeAsync(entities, token);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default)
    {
        return await DbSet.Where(predicate).ToListAsync(token);
    }

    public async ValueTask<TEntity?> GetAsync(string id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken token = default)
    {
        return await DbSet.ToListAsync(token);
    }

    public async Task RemoveAsync(string id)
    {
        var entity = await GetAsync(id);
        if (entity is not null)
            DbSet.Remove(entity);
    }

    public Task UpsertAsync(TEntity entity, CancellationToken token = default)
    {
        throw new NotImplementedException();
    } 
}