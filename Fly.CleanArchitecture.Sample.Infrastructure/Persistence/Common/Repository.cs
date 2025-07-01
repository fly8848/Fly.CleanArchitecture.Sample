using Fly.CleanArchitecture.Sample.Application.Common;
using Fly.CleanArchitecture.Sample.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence.Common;

public abstract class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbContext _dbContext;

    protected Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public Task<T> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public Task<List<T>> UpdateRangeAsync(List<T> entities)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        return Task.FromResult(entities);
    }

    public Task<T> DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.FromResult(entity);
    }

    public Task<List<T>> DeleteRangeAsync(List<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        return Task.FromResult(entities);
    }

    public async Task<T?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return await queryable.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> SingleOrDefaultAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return await queryable.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return await queryable.CountAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return await queryable.AnyAsync(cancellationToken);
    }

    public Task<IQueryable<T>> AsQueryableAsync()
    {
        return Task.FromResult(_dbContext.Set<T>().AsQueryable());
    }
}