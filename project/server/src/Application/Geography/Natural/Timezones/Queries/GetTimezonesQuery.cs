// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Includes.Timezones;
using server.src.Application.Geography.Natural.Timezones.Filters;
using server.src.Application.Geography.Natural.Timezones.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Timezones.Queries;

public record GetTimezonesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemTimezoneDto>>>;

public class GetTimezonesHandler : IRequestHandler<GetTimezonesQuery, ListResponse<List<ListItemTimezoneDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTimezonesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTimezoneDto>>> Handle(GetTimezonesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TimezoneFilters.TimezoneNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Timezone, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TimezoneSearchFilter();
        var filters = new Expression<Func<Timezone, bool>>[] { filter! };

        // Include related entities
        var includes = TimezonesIncludes.GetTimezonesIncludes();

        // Fetch paginated results
        var pagedTimezones = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedTimezones.Rows.Select(t => t.ListItemTimezoneDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedTimezones.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemTimezoneDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Timezones fetched successfully." 
                : "No timezones found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTimezones.UrlQuery.TotalRecords,
                PageSize = pagedTimezones.UrlQuery.PageSize,
                PageNumber = pagedTimezones.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}