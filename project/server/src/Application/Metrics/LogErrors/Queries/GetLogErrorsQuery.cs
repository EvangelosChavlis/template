// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Metrics.LogErrors.Includes;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Metrics.LogErrors.Dtos;
using server.src.Persistence.Common.Interfaces;
using server.src.Application.Metrics.LogErrors.Filters;
using server.src.Domain.Metrics.LogErrors.Models;
using server.src.Application.Metrics.LogErrors.Mappings;

namespace server.src.Application.Metrics.LogErrors.Queries;

public record GetLogErrorsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemLogErrorDto>>>;

public class GetLogErrorsHandler : IRequestHandler<GetLogErrorsQuery, ListResponse<List<ListItemLogErrorDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetLogErrorsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemLogErrorDto>>> Handle(GetLogErrorsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = LogErrorFiltrers.LogErrorNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<LogError, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.LogErrorSearchFilter();

        var filters = new Expression<Func<LogError, bool>>[] { filter! };

        // Including
        var includes = LogErrorIncludes.GetLogErrorsIncludes();

        // Paging
        var pagedErrors = await _commonRepository.GetPagedResultsAsync(pageParams, filters, token: token);
        // Mapping
        var dto = pagedErrors.Rows.Select(e => e.ListItemLogErrorDtoMapping()).ToList();

        // Determine success 
        var success = pagedErrors.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemLogErrorDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Errors fetched successfully." : 
                "No errors found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedErrors.UrlQuery.TotalRecords,
                PageSize = pagedErrors.UrlQuery.PageSize,
                PageNumber = pagedErrors.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}