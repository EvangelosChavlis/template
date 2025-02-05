namespace server.src.Persistence.Common.Interfaces;

/// <summary>
/// Represents a unit of work that manages transactions and ensures atomic operations in the database.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Commits all changes made within the unit of work to the database.
    /// </summary>
    /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the commit was successful.</returns>
    Task<bool> CommitAsync(CancellationToken token = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task BeginTransactionAsync(CancellationToken token = default);

    /// <summary>
    /// Commits the current database transaction, making all changes permanent.
    /// </summary>
    /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CommitTransactionAsync(CancellationToken token = default);

    /// <summary>
    /// Rolls back the current database transaction, reverting all changes made within the transaction.
    /// </summary>
    /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RollbackTransactionAsync(CancellationToken token = default);
}
