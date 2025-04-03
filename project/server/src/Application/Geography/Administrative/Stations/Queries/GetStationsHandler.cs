// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Includes.Stations;
using server.src.Application.Geography.Administrative.Stations.Filters;
using server.src.Application.Geography.Natural.Stations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Stations.Queries;

public record GetStationsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemStationDto>>>;

public class GetStationsHandler : IRequestHandler<GetStationsQuery, ListResponse<List<ListItemStationDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetStationsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemStationDto>>> Handle(GetStationsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = StationFilters.StationNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Station, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.StationSearchFilter();
        var filters = new Expression<Func<Station, bool>>[] { filter! };

        // Include related entities
        var includes = StationsIncludes.GetStationsIncludes();

        // Fetch paginated results
        var pagedStations = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedStations.Rows.Select(t => t.ListItemStationDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedStations.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemStationDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Stations fetched successfully." 
                : "No stations found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedStations.UrlQuery.TotalRecords,
                PageSize = pagedStations.UrlQuery.PageSize,
                PageNumber = pagedStations.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}