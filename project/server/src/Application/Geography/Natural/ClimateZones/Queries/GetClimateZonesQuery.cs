// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Includes.ClimateZones;
using server.src.Application.Geography.Natural.ClimateZones.Filters;
using server.src.Application.Geography.Natural.ClimateZones.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.ClimateZones.Queries;

public record GetClimateZonesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemClimateZoneDto>>>;

public class GetClimateZonesHandler : IRequestHandler<GetClimateZonesQuery, ListResponse<List<ListItemClimateZoneDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetClimateZonesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemClimateZoneDto>>> Handle(GetClimateZonesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ClimateZoneFilters.ClimateZoneNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<ClimateZone, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ClimateZoneSearchFilter();
        var filters = new Expression<Func<ClimateZone, bool>>[] { filter! };

        // Include related entities
        var includes = ClimateZonesIncludes.GetClimateZonesIncludes();

        // Fetch paginated results
        var pagedClimateZones = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedClimateZones.Rows.Select(c => c.ListItemClimateZoneDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedClimateZones.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemClimateZoneDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Climate zones fetched successfully." 
                : "No climate zones found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedClimateZones.UrlQuery.TotalRecords,
                PageSize = pagedClimateZones.UrlQuery.PageSize,
                PageNumber = pagedClimateZones.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
