// packages
using System.Linq.Expressions;
using System.Net;
using server.src.Application.Auth.Users.Projections;


// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Metrics.TelemetryRecords.Mappings;
using server.src.Application.Metrics.TelemetryRecords.Projections;
using server.src.Application.TelemetryRecords.Filters;
using server.src.Application.TelemetryRecords.Includes;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Metrics.Telemetry.Queries;

public record GetTelemetryRecordByUserIdQuery(
    Guid Id, 
    UrlQuery UrlQuery
) : IRequest<ListResponse<List<ListItemTelemetryRecordDto>>>;

public class GetTelemetryRecordByUserIdHandler : 
    IRequestHandler<GetTelemetryRecordByUserIdQuery, ListResponse<List<ListItemTelemetryRecordDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTelemetryRecordByUserIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTelemetryRecordDto>>> Handle(GetTelemetryRecordByUserIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var userProjection = UserProjections.GetUserProjection();
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync<User>(
            userFilters,
            projection: userProjection, 
            token: token
        );

        // Check for existence
        if (user is null)
            return new ListResponse<List<ListItemTelemetryRecordDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

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

        var filters = filter.TelemetryRecordMatchFilters(user.Id);

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
