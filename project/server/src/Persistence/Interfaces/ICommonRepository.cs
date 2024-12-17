// packages
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Models.Common;

namespace server.src.Persistence.Interfaces;

public interface ICommonRepository
{
    public Task<Envelope<T>> GetPagedResultsAsync<T>(
        DbSet<T> query,
        UrlQuery pageParams,
        Expression<Func<T, bool>>[] filterExpressions,
        IncludeThenInclude<T>[] includeThenIncludeExpressions,
        CancellationToken token = default
    ) where T : class ;

    Task<List<T>> GetResultPickerAsync<T>(DbSet<T> query, CancellationToken token = default) 
        where T : class;

    Task<T?> GetResultByIdAsync<T>(
        DbSet<T> query,
        Expression<Func<T, bool>>[] filterExpressions,
        Expression<Func<T, object>>[] includeExpressions,
        CancellationToken token = default
    ) where T : class ;

    Task<int> GetCountAsync<T>(
        DbSet<T> query,
        Expression<Func<T, bool>>[] filterExpressions,
        CancellationToken token = default
    ) where T : class ;
}