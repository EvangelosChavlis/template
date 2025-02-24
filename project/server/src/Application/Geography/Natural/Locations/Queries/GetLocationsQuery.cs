// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Includes.Locations;
using server.src.Application.Geography.Natural.Locations.Filters;
using server.src.Application.Geography.Natural.Locations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Locations.Queries;

public record GetLocationsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemLocationDto>>>;

public class GetLocationsHandler : IRequestHandler<GetLocationsQuery, ListResponse<List<ListItemLocationDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetLocationsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemLocationDto>>> Handle(GetLocationsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = LocationFilters.LocationSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Location, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.LocationSearchFilter();
        var filters = new Expression<Func<Location, bool>>[] { filter! };

        // Include related entities
        var includes = LocationsIncludes.GetLocationsIncludes();

        // Fetch paginated results
        var pagedLocations = await _commonRepository.GetPagedResultsAsync(pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedLocations.Rows.Select(t => t.ListItemLocationDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedLocations.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemLocationDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Locations fetched successfully." 
                : "No locations found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedLocations.UrlQuery.TotalRecords,
                PageSize = pagedLocations.UrlQuery.PageSize,
                PageNumber = pagedLocations.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}