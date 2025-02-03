// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Common;

namespace server.src.Persistence.Interfaces;

/// <summary>
/// Provides a common repository interface for data access operations, including querying, adding, updating, and deleting entities.
/// </summary>
public interface ICommonRepository
{
    /// <summary>
    /// Retrieves a paginated list of results from a queryable data source.
    /// </summary>
    /// <typeparam name="T">The type of the data being queried.</typeparam>
    /// <param name="pageParams">Query parameters for pagination and filtering.</param>
    /// <param name="filterExpressions">An array of filter expressions to apply to the query.</param>
    /// <param name="includeThenIncludeExpressions">An array of include expressions for related entities.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an envelope of paginated results.</returns>
    Task<Envelope<T>> GetPagedResultsAsync<T>(
        UrlQuery pageParams,
        Expression<Func<T, bool>>[] filterExpressions,
        IncludeThenInclude<T>[] includeThenIncludeExpressions,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Retrieves a list of results from a queryable data source based on the provided filter expressions, without applying pagination.
    /// </summary>
    /// <typeparam name="T">The type of the data being queried.</typeparam>
    /// <param name="filterExpressions">An array of filter expressions used to refine the query results.</param>
    /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of matching results.</returns>
    Task<List<T>> GetResultPickerAsync<T>(
        Expression<Func<T, bool>>[] filterExpressions, 
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Retrieves a single result from a queryable data source based on its identifier and additional filtering.
    /// </summary>
    /// <typeparam name="T">The type of the data being queried.</typeparam>
    /// <param name="filterExpressions">An array of filter expressions to apply to the query.</param>
    /// <param name="includeExpressions">An array of expressions specifying related entities to include in the query.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result or null if not found.</returns>
    Task<T?> GetResultByIdAsync<T>(
        Expression<Func<T, bool>>[] filterExpressions,
        Expression<Func<T, object>>[] includeExpressions,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Counts the number of entities in a queryable data source that match specified filter criteria.
    /// </summary>
    /// <typeparam name="T">The type of the data being queried.</typeparam>
    /// <param name="filterExpressions">An array of filter expressions to apply to the query.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of matching entities.</returns>
    Task<int> GetCountAsync<T>(
        Expression<Func<T, bool>>[] filterExpressions,
        CancellationToken token = default
    ) where T : class;

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <typeparam name="T">The type of entity to add.</typeparam>
    /// <param name="entity">The entity to be added.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
    Task<bool> AddAsync<T>(T entity, CancellationToken token = default) 
        where T : class;

    /// <summary>
    /// Adds multiple entities to the database in a single operation.
    /// </summary>
    /// <typeparam name="T">The type of entities to add.</typeparam>
    /// <param name="entities">A list of entities to be added.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
    Task<bool> AddRangeAsync<T>(List<T> entities, CancellationToken token = default) 
        where T : class;

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <typeparam name="T">The type of entity to update.</typeparam>
    /// <param name="entity">The entity with updated values.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
    Task<bool> UpdateAsync<T>(T entity, CancellationToken token = default) 
        where T : class;

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <typeparam name="T">The type of entity to delete.</typeparam>
    /// <param name="entity">The entity to be deleted.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
    Task<bool> DeleteAsync<T>(T entity, CancellationToken token = default) 
        where T : class;
}
