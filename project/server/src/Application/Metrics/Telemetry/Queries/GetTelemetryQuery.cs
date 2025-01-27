// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Domain.Models.Metrics;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.Persistence.Interfaces;
using server.src.Domain.Dto.Metrics;
using server.src.Application.Telemetry.Filters;
using server.src.Application.Telemetry.Includes;

namespace server.src.Application.Metrics.Telemetry.Queries;

public record GetTelemetryQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemTelemetryDto>>>;

public class GetTelemetryHandler : IRequestHandler<GetTelemetryQuery, ListResponse<List<ListItemTelemetryDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetTelemetryHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTelemetryDto>>> Handle(GetTelemetryQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TelemetryFiltrers.TelemetryNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Telemetry, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TelemetrySearchFilter();

        var filters = new Expression<Func<Telemetry, bool>>[] { filter! };

        // Including
        var includes = TelemetryIncludes.GetTelemetryIncludes();

        // Paging
        var pagedTelemetry = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedTelemetry.Rows.Select(t => t.ListItemTelemetryDtoMapping()).ToList();

        // Determine success 
        var success = pagedTelemetry.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemTelemetryDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Telemetry data fetched successfully." : 
                "No telemetry data found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTelemetry.UrlQuery.TotalRecords,
                PageSize = pagedTelemetry.UrlQuery.PageSize,
                PageNumber = pagedTelemetry.UrlQuery.PageNumber ?? 1,
            }
        };
    }

}