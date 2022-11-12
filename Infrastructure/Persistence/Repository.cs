using System.Linq.Expressions;
using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbContext Context;
    internal readonly DbSet<TEntity> DbSet;

    public Repository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetAsync(string id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task RemoveAsync(string id)
    {
        var entity = await GetAsync(id);
        DbSet.Remove(entity);
    }

    public Task UpsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    } 
}