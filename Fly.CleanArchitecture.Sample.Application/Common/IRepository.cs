using Fly.CleanArchitecture.Sample.Domain.Common;

namespace Fly.CleanArchitecture.Sample.Application.Common;

public interface IRepository<T> where T : Entity
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> UpdateRangeAsync(List<T> entities);
    Task<T> DeleteAsync(T entity);
    Task<List<T>> DeleteRangeAsync(List<T> entities);
    Task<T?> FindAsync(object id, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default);
    Task<T?> SingleOrDefaultAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default);
    Task<int> CountAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(IQueryable<T> queryable, CancellationToken cancellationToken = default);
    Task<IQueryable<T>> AsQueryableAsync();
}