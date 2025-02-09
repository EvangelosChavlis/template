// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Common.Models;

namespace server.src.Persistence.Common.Interfaces;

/// <summary>
/// Provides a common repository interface for data access operations, including querying, adding, updating, and deleting entities.
/// </summary>
public interface ICommonRepository
{
    /// <summary>
    /// Retrieves a paginated list of results from the data source, applying optional filters, includes, and projections.
    /// </summary>
    /// <typeparam name="T">The type of the entity being queried.</typeparam>
    /// <param name="pageParams">Pagination and sorting parameters.</param>
    /// <param name="filterExpressions">Optional filter conditions to apply to the query.</param>
    /// <param name="includeThenIncludeExpressions">Optional related entity includes with nested includes.</param>
    /// <param name="projection">Optional projection to transform the result.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>An envelope containing paginated results.</returns>
    Task<Envelope<T>> GetPagedResultsAsync<T>(
        UrlQuery pageParams,
        Expression<Func<T, bool>>[]? filterExpressions = default,
        IncludeThenInclude<T>[]? includeThenIncludeExpressions = default,
        Expression<Func<T, T>>? projection = default,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Retrieves a list of entities from the data source based on optional filter criteria and projections.
    /// </summary>
    /// <typeparam name="T">The type of the entity being queried.</typeparam>
    /// <param name="filterExpressions">Optional filter conditions.</param>
    /// <param name="projection">Optional projection to transform the result.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A list of matching entities.</returns>
    Task<List<T>> GetResultPickerAsync<T>(
        Expression<Func<T, bool>>[]? filterExpressions = default,
        Expression<Func<T, T>>? projection = default, 
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Retrieves a single entity based on filtering criteria, including optional related entities and projections.
    /// </summary>
    /// <typeparam name="T">The type of the entity being queried.</typeparam>
    /// <param name="filterExpressions">Optional filter conditions.</param>
    /// <param name="includeExpressions">Optional related entity includes.</param>
    /// <param name="projection">Optional projection to shape the returned result.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The matching entity or null if not found.</returns>
    Task<T?> GetResultByIdAsync<T>(
        Expression<Func<T, bool>>[]? filterExpressions = default,
        Expression<Func<T, object>>[]? includeExpressions = default,
        Expression<Func<T, T>>? projection = default,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Counts the number of entities matching the specified filter criteria.
    /// </summary>
    /// <typeparam name="T">The type of the entity being queried.</typeparam>
    /// <param name="filterExpressions">Optional filter conditions.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The count of matching entities.</returns>
    Task<int> GetCountAsync<T>(
        Expression<Func<T, bool>>[] filterExpressions,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Determines whether any entity matches the specified filter criteria.
    /// </summary>
    /// <typeparam name="T">The type of the entity being queried.</typeparam>
    /// <param name="filterExpressions">Optional filter conditions.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if at least one matching entity exists; otherwise, false.</returns>
    Task<bool> AnyExistsAsync<T>(
        Expression<Func<T, bool>>[] filterExpressions,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <typeparam name="T">The type of entity to add.</typeparam>
    /// <param name="entity">The entity to be added.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if the operation was successful; otherwise, false.</returns>
    Task<bool> AddAsync<T>(
        T entity, 
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Adds multiple entities to the database in a single operation.
    /// </summary>
    /// <typeparam name="T">The type of entities to add.</typeparam>
    /// <param name="entities">The list of entities to be added.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if the operation was successful; otherwise, false.</returns>
    Task<bool> AddRangeAsync<T>(
        List<T> entities, 
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <typeparam name="T">The type of entity to update.</typeparam>
    /// <param name="entity">The entity with updated values.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if the operation was successful; otherwise, false.</returns>
    Task<bool> UpdateAsync<T>(
        T entity, 
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <typeparam name="T">The type of entity to delete.</typeparam>
    /// <param name="entity">The entity to be deleted.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if the operation was successful; otherwise, false.</returns>
    Task<bool> DeleteAsync<T>(
        T entity, 
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Locks an entity for a specified duration, preventing concurrent modifications.
    /// </summary>
    /// <typeparam name="T">The type of entity being locked.</typeparam>
    /// <param name="entityId">The ID of the entity to lock.</param>
    /// <param name="userId">The ID of the user acquiring the lock.</param>
    /// <param name="duration">The duration of the lock.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if the lock was acquired successfully; otherwise, false.</returns>
    Task<bool> LockAsync<T>(
        Guid entityId, 
        Guid userId, 
        TimeSpan duration, 
        CancellationToken token = default
    ) where T : BaseEntity;
    
    /// <summary>
    /// Unlocks a previously locked entity, allowing modifications.
    /// </summary>
    /// <typeparam name="T">The type of entity being unlocked.</typeparam>
    /// <param name="entityId">The ID of the entity to unlock.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>True if the unlock operation was successful; otherwise, false.</returns>
    Task<bool> UnlockAsync<T>(
        Guid entityId, 
        CancellationToken token = default
    ) where T : BaseEntity;
}