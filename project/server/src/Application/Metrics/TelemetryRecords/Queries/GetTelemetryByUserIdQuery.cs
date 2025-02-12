// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Telemetry.Filters;
using server.src.Application.Telemetry.Includes;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Metrics;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Metrics.Telemetry.Queries;

public record GetTelemetryByUserIdQuery(Guid Id, UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemTelemetryDto>>>;

public class GetTelemetryByUserIdHandler : IRequestHandler<GetTelemetryByUserIdQuery, ListResponse<List<ListItemTelemetryDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTelemetryByUserIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTelemetryDto>>> Handle(GetTelemetryByUserIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
            return new ListResponse<List<ListItemTelemetryDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);

        var pageParams = query.UrlQuery;
                
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TelemetryFiltrers.TelemetryRecordNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Telemetry, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TelemetrySearchFilter();

        var filters = filter.TelemetryMatchFilters(user.Id);

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
