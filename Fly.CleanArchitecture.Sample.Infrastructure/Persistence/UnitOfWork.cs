using Fly.CleanArchitecture.Sample.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fly.CleanArchitecture.Sample.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            throw new ArgumentNullException(nameof(_transaction));
        }

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
        {
            throw new ArgumentNullException(nameof(_transaction));
        }

        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
        {
            throw new ArgumentNullException(nameof(_transaction));
        }

        await _transaction.RollbackAsync();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _transaction = null;
    }
}