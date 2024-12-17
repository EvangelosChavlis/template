// packages
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;

namespace server.src.Persistence.Repositories;

public class CommonRepository : ICommonRepository
{
    public async Task<Envelope<T>> GetPagedResultsAsync<T>(
        DbSet<T> query,
        UrlQuery pageParams,
        Expression<Func<T, bool>>[] filterExpressions,
        IncludeThenInclude<T>[] includeThenIncludeExpressions,
        CancellationToken token = default
    ) where T : class
    {
        // Initialize paging properties
        var pageNum = pageParams.PageNumber ?? 1;
        var pageSize = pageParams.PageSize;

        // Filtering query
        var filteredQuery = query.AsQueryable();

        if (filterExpressions != null)
        {
            foreach (var filterExpression in filterExpressions)
            {
                if (filterExpression != null)
                    filteredQuery = filteredQuery.Where(filterExpression);
            }
        }

        // Calculating records
        var totalRecords = await filteredQuery.CountAsync(token);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        // Ordering query
        IQueryable<T> orderedQuery = pageParams.SortDescending
            ? filteredQuery.OrderByDescending(x => EF.Property<object>(x, pageParams.SortBy!))
            : filteredQuery.OrderBy(x => EF.Property<object>(x, pageParams.SortBy!));

        // Apply the include and thenInclude expressions to the query
        if (includeThenIncludeExpressions != null)
        {
            foreach (var includeThenInclude in includeThenIncludeExpressions)
            {
                var includableQuery = orderedQuery.Include(includeThenInclude.Include);

                if (includeThenInclude.ThenIncludes != null)
                {
                    foreach (var thenInclude in includeThenInclude.ThenIncludes)
                    {
                        includableQuery = includableQuery.ThenInclude(thenInclude);
                    }
                }

                orderedQuery = includableQuery;
            }
        }

        // Paging query
        var pagedData = await orderedQuery
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        pageParams.TotalRecords = totalRecords;

        // Initializing object
        var envelope = new Envelope<T>
        {
            Rows = pagedData,
            UrlQuery = pageParams
        };

        return envelope;
    }

    public async Task<List<T>> GetResultPickerAsync<T>(DbSet<T> query, CancellationToken token = default) 
        where T : class
    {
        var elements = await query.ToListAsync(token);

        return elements;
    }

    public async Task<T?> GetResultByIdAsync<T>(
        DbSet<T> query, 
        Expression<Func<T, bool>>[] filterExpressions,
        Expression<Func<T, object>>[] includeExpressions,
        CancellationToken token = default
    ) where T : class
    {
        // Filtering query
        var filteredQuery = query.AsQueryable();

        if (filterExpressions is not null)
        {
            foreach (var filterExpression in filterExpressions)
            {
                if (filterExpression is not null)
                    filteredQuery = filteredQuery.Where(filterExpression);
            }
        }

        // Apply the include expressions to the query
        if (includeExpressions is not null)
        {
            foreach (var includeExpression in includeExpressions)
                filteredQuery = filteredQuery.Include(includeExpression);
        }

        var item = await filteredQuery.FirstOrDefaultAsync(token);

        return item;
    }

    public async Task<int> GetCountAsync<T>(DbSet<T> query, 
        Expression<Func<T, bool>>[] filterExpressions, 
        CancellationToken token = default
    ) where T : class
    {
        // Filtering query
        var filteredQuery = query.AsQueryable();

        if (filterExpressions is not null)
        {
            foreach (var filterExpression in filterExpressions)
            {
                if (filterExpression is not null)
                    filteredQuery = filteredQuery.Where(filterExpression);
            }
        }

        var count = await filteredQuery.CountAsync(token);

        return count;
    }
}