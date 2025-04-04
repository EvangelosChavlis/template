// packages
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;


// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Common.Models;
using server.src.Persistence.Common.Contexts;
using server.src.Persistence.Common.Helpers;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Persistence.Common.Repositories;

public class CommonRepository : ICommonRepository
{
    private readonly DataContext _context;
    private readonly ArchiveContext _archiveContext;
    private readonly IAuditLogHelper _auditLogHelper;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public CommonRepository(DataContext context, ArchiveContext archiveContext,
        IAuditLogHelper auditLogHelper, IUnitOfWork unitOfWork)
    {
        _context = context;
        _archiveContext = archiveContext;
        _auditLogHelper = auditLogHelper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Envelope<T>> GetPagedResultsAsync<T>(
        UrlQuery pageParams,
        Expression<Func<T, bool>>[]? filterExpressions = default,
        IncludeThenInclude<T>[]? includeThenIncludeExpressions = default,
        Expression<Func<T, T>>? projection = default,
        CancellationToken token = default
    ) where T : class
    {
        // Initialize paging properties
        var pageNum = pageParams.PageNumber ?? 1;
        var pageSize = pageParams.PageSize;

        // Retrieve the dataset dynamically
        IQueryable<T> query = _context.Set<T>();

        // Apply filtering
        if (filterExpressions != null)
        {
            foreach (var filterExpression in filterExpressions)
            {
                if (filterExpression != null)
                    query = query.Where(filterExpression);
            }
        }

        // Calculate total records and pages
        var totalRecords = await query.CountAsync(token);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        // Apply sorting
        query = pageParams.SortDescending
            ? query.OrderByDescending(x => EF.Property<object>(x, pageParams.SortBy!))
            : query.OrderBy(x => EF.Property<object>(x, pageParams.SortBy!));

        // Apply includes and thenIncludes
        if (includeThenIncludeExpressions is not null)
        {
            foreach (var includeThenInclude in includeThenIncludeExpressions)
            {
                var includableQuery = query.Include(includeThenInclude.Include);

                if (includeThenInclude.ThenIncludes != null)
                {
                    foreach (var thenInclude in includeThenInclude.ThenIncludes)
                    {
                        includableQuery = includableQuery.ThenInclude(thenInclude);
                    }
                }

                query = includableQuery;
            }
        }

        // Apply projection
        if (projection is not null)
            query = query.Select(projection);

        // Apply pagination
        var pagedData = await query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        pageParams.TotalRecords = totalRecords;

        // Initialize response
        return new Envelope<T>
        {
            Rows = pagedData,
            UrlQuery = pageParams
        };
    }


    public async Task<List<TResult>> GetResultPickerAsync<T, TResult>(
        Expression<Func<T, bool>>[]? filterExpressions = default,
        IncludeThenInclude<T>[]? includeThenIncludeExpressions = default,
        Expression<Func<T, TResult>>? projection = default,
        CancellationToken token = default
    ) where T : class
    {
        IQueryable<T> query = _context.Set<T>();

        // Apply filtering
        if (filterExpressions is not null)
        {
            foreach (var filterExpression in filterExpressions)
            {
                if (filterExpression is not null)
                    query = query.Where(filterExpression);
            }
        }

        // Apply includes and thenIncludes
        if (includeThenIncludeExpressions is not null)
        {
            foreach (var includeThenInclude in includeThenIncludeExpressions)
            {
                var includableQuery = query.Include(includeThenInclude.Include);

                if (includeThenInclude.ThenIncludes != null)
                {
                    foreach (var thenInclude in includeThenInclude.ThenIncludes)
                    {
                        includableQuery = includableQuery.ThenInclude(thenInclude);
                    }
                }

                query = includableQuery;
            }
        }

        // Apply projection
        if (projection is not null)
        {
            var projectedQuery = query.Select(projection);
            return await projectedQuery.ToListAsync(token);
        }

        return await query.Cast<TResult>().ToListAsync(token);
    }

    public async Task<T?> GetResultByIdAsync<T>(
        Expression<Func<T, bool>>[]? filters = default,
        Expression<Func<T, object>>[]? includes = default,
        Expression<Func<T, T>>? projection = default,
        CancellationToken token = default
    ) where T : class
    {
        IQueryable<T> query = _context.Set<T>();

        // Apply filtering
        if (filters is not null)
        {
            foreach (var filterExpression in filters)
            {
                if (filterExpression is not null)
                    query = query.Where(filterExpression);
            }
        }

        // Apply includes
        if (includes is not null)
        {
            foreach (var includeExpression in includes)
                query = query.Include(includeExpression);
        }
        
        // Apply projection
        if (projection is not null)
            query = query.Select(projection);

        return await query
            .SingleOrDefaultAsync(token);
    }

    public async Task<TResult?> GetResultByIdAsync<T, TResult>(
        Expression<Func<T, bool>>[]? filters = default,
        Expression<Func<T, object>>[]? includes = default,
        Expression<Func<T, TResult>>? projection = default,
        CancellationToken token = default
    ) where T : class
    {
        IQueryable<T> query = _context.Set<T>();

        // Apply filtering
        if (filters is not null)
        {
            foreach (var filterExpression in filters)
            {
                if (filterExpression is not null)
                    query = query.Where(filterExpression);
            }
        }

        // Apply includes
        if (includes is not null)
        {
            foreach (var includeExpression in includes)
                query = query.Include(includeExpression);
        }

        // Apply projection
        if (projection is not null)
        {
            var projectedQuery = query.Select(projection);
            return await projectedQuery.Cast<TResult>().SingleOrDefaultAsync(token);
        }

        return await query.Cast<TResult>().SingleOrDefaultAsync(token);
        
    }


    public async Task<int> GetCountAsync<T>(
        Expression<Func<T, bool>>[] filterExpressions,
        CancellationToken token = default
    ) where T : class
    {
        IQueryable<T> query = _context.Set<T>();

        if (filterExpressions is not null)
        {
            foreach (var filterExpression in filterExpressions)
            {
                if (filterExpression is not null)
                    query = query.Where(filterExpression);
            }
        }

        return await query.CountAsync(token);
    }

    public async Task<bool> AnyExistsAsync<T>(
        Expression<Func<T, bool>>[]? filters = default,
        CancellationToken token = default
    ) where T : class
    {
        IQueryable<T> query = _context.Set<T>();

        if (filters is not null)
        {
            foreach (var filterExpression in filters)
            {
                if (filterExpression is not null)
                    query = query.Where(filterExpression);
            }
        }

        return await query.AnyAsync(token);
    }

    public async Task<bool> AddAsync<T>(
        T entity, 
        CancellationToken token = default
    ) where T : class
    {
        await _context.Set<T>().AddAsync(entity, token);
        
        var result = await _unitOfWork.CommitAsync(token);

        if (result)
            await _auditLogHelper.CreateAuditLogAsync(null, entity, token);

        return result;
    }

    public async Task<bool> AddRangeAsync<T>(
        List<T> entities, 
        CancellationToken token = default
    ) where T : class
    {
        if (entities is null || entities.Count is 0) 
            return false;

        await _context.Set<T>().AddRangeAsync(entities, token);
        
        var result = await _unitOfWork.CommitAsync(token);

        if (result)
        {
            foreach (var entity in entities)
                await _auditLogHelper.CreateAuditLogAsync(null, entity, token);
        }

        return result;
    }


    public async Task<bool> UpdateAsync<T>(
        T entity, 
        CancellationToken token = default
    ) where T : class
    {
        var idProperty = entity.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
        var entityId = idProperty?.GetValue(entity);

        if (entityId is null) return false;

        var existingEntity = await _context.Set<T>().FindAsync([entityId], token);

        if (existingEntity is null) return false;

        _context.Set<T>().Update(entity);
        var result = await _unitOfWork.CommitAsync(token);

        if (result)
            await _auditLogHelper.CreateAuditLogAsync(existingEntity, entity, token);

        return result;
    }


    public async Task<bool> DeleteAsync<T>(
        T entity, 
        CancellationToken token = default
    ) where T : class
    {
        // Archive entity
        await _archiveContext.Set<T>().AddAsync(entity, token);

        // Remove entity
        _context.Set<T>().Remove(entity);
        var result = await _unitOfWork.CommitAsync(token);

        if (result)
            await _auditLogHelper.CreateAuditLogAsync(entity, null, token);

        return result;
    }

    public async Task<bool> LockAsync<T>(
        Guid entityId, 
        Guid userId, 
        TimeSpan duration, 
        CancellationToken token = default
    ) where T : BaseEntity
    {
        var dbSet = _context.Set<T>();

        var existingEntity = await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(entityId), token);
        
        if (existingEntity is null) return false;

        if (existingEntity.IsLocked()) return false;

        var result = await dbSet
            .Where(e => EF.Property<object>(e, "Id").Equals(entityId))
            .ExecuteUpdateAsync(update => update
                .SetProperty(e => e.LockUntil, DateTime.UtcNow.Add(duration))
                .SetProperty(e => e.UserLockedId, userId), token);

        if (result > 0)
        {
            var commitResult = await _unitOfWork.CommitAsync(token);
            if (commitResult)
            {
                var updatedEntity = await dbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(entityId), token);

                if (updatedEntity is not null)
                    await _auditLogHelper.CreateAuditLogAsync(existingEntity, updatedEntity, token);
            }
            return commitResult;
        }

        return false;
    }

    public async Task<bool> UnlockAsync<T>(
        Guid entityId, 
        CancellationToken token = default
    ) where T : BaseEntity
    {
        var dbSet = _context.Set<T>();

        var existingEntity = await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(entityId), token);

        if (existingEntity is null) return false;

        if (!existingEntity.IsLocked()) return false;

        var result = await dbSet
            .Where(e => EF.Property<object>(e, "Id").Equals(entityId))
            .ExecuteUpdateAsync(update => update
                .SetProperty(e => e.LockUntil, (DateTime?)null)
                .SetProperty(e => e.UserLockedId, (Guid?)null),
            token);

        if (result > 0)
        {
            var commitResult = await _unitOfWork.CommitAsync(token);
            if (commitResult)
            {
                var updatedEntity = await dbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(entityId), token);

                if (updatedEntity is not null)
                    await _auditLogHelper.CreateAuditLogAsync(existingEntity, updatedEntity, token);
            }
            return commitResult;
        }

        return false;
    }
}