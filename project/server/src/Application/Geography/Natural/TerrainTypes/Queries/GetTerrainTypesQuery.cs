// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Includes.TerrainTypes;
using server.src.Application.Geography.Natural.TerrainTypes.Filters;
using server.src.Application.Geography.Natural.TerrainTypes.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.TerrainTypes.Queries;

public record GetTerrainTypesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemTerrainTypeDto>>>;

public class GetTerrainTypesHandler : IRequestHandler<GetTerrainTypesQuery, ListResponse<List<ListItemTerrainTypeDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTerrainTypesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTerrainTypeDto>>> Handle(GetTerrainTypesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TerrainTypeFilters.TerrainTypeNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<TerrainType, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TerrainTypeSearchFilter();
        var filters = new Expression<Func<TerrainType, bool>>[] { filter! };

        // Include related entities
        var includes = TerrainTypesIncludes.GetTerrainTypesIncludes();

        // Fetch paginated results
        var pagedTerrainTypes = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedTerrainTypes.Rows.Select(w => w.ListItemTerrainTypeDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedTerrainTypes.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemTerrainTypeDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Terrain types fetched successfully." 
                : "No terrain types found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTerrainTypes.UrlQuery.TotalRecords,
                PageSize = pagedTerrainTypes.UrlQuery.PageSize,
                PageNumber = pagedTerrainTypes.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
