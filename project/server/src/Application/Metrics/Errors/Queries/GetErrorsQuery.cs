// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Metrics.Errors.Filters;
using server.src.Application.Metrics.Errors.Includes;
using server.src.Application.Metrics.Errors.Mappings;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Errors;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Metrics.Errors.Queries;

public record GetErrorsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemErrorDto>>>;

public class GetErrorsHandler : IRequestHandler<GetErrorsQuery, ListResponse<List<ListItemErrorDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetErrorsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemErrorDto>>> Handle(GetErrorsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ErrorsFiltrers.ErrorNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<LogError, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ErrorSearchFilter();

        var filters = new Expression<Func<LogError, bool>>[] { filter! };

        // Including
        var includes = ErrorsIncludes.GetErrorsIncludes();

        // Paging
        var pagedErrors = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedErrors.Rows.Select(e => e.ListItemErrorDtoMapping()).ToList();

        // Determine success 
        var success = pagedErrors.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemErrorDto>>()
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