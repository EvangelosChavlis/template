// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.TelemetryRecords.Filters;
using server.src.Application.TelemetryRecords.Includes;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Metrics.TelemetryRecords.Models;
using server.src.Application.Metrics.TelemetryRecords.Projections;
using server.src.Application.Metrics.TelemetryRecords.Mappings;

namespace server.src.Application.Metrics.TelemetryRecords.Queries;

public record GetTelemetryRecordsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemTelemetryRecordDto>>>;

public class GetTelemetryRecordsHandler : IRequestHandler<GetTelemetryRecordsQuery, ListResponse<List<ListItemTelemetryRecordDto>>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetTelemetryRecordsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTelemetryRecordDto>>> Handle(GetTelemetryRecordsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TelemetryRecordFiltrers.TelemetryRecordNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<TelemetryRecord, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TelemetryRecordSearchFilter();

        var filters = new Expression<Func<TelemetryRecord, bool>>[] { filter! };

        // Including
        var includes = TelemetryRecordIncludes.GetTelemetryRecordIncludes();
        // Projection
        var projection = TelemetryRecordProjections.GetTelemetryRecordsProjection();
        // Paging
        var pagedTelemetry = await _commonRepository.GetPagedResultsAsync(
            pageParams, 
            filters, 
            includes, 
            projection,
            token
        );
        // Mapping
        var dto = pagedTelemetry.Rows
            .Select(t => t.ListItemTelemetryRecordDtoMapping())
            .ToList();

        // Determine success 
        var success = pagedTelemetry.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemTelemetryRecordDto>>()
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