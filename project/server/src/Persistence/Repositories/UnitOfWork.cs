// packages
using Microsoft.EntityFrameworkCore.Storage;

// source
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public UnitOfWork() { }

    public async Task<bool> CommitAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token) > 0;
    }

    public async Task BeginTransactionAsync(CancellationToken token = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(token);
    }

    public async Task CommitTransactionAsync(CancellationToken token = default)
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync(token);
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken token = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(token);
            await _transaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}