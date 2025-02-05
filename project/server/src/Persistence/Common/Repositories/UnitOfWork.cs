// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using server.src.Persistence.Common.Interfaces;


// source
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Persistence.Common.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public UnitOfWork() { }

    public async Task<bool> CommitAsync(DbContext context, CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token) > 0;
    }

    public async Task BeginTransactionAsync(DbContext context, CancellationToken token = default)
    {
        _transaction = await context.D.BeginTransactionAsync(token);
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