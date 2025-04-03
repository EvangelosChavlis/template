// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Includes.SurfaceTypes;
using server.src.Application.Geography.Natural.SurfaceTypes.Filters;
using server.src.Application.Geography.Natural.SurfaceTypes.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Queries;

public record GetSurfaceTypesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemSurfaceTypeDto>>>;

public class GetSurfaceTypesHandler : IRequestHandler<GetSurfaceTypesQuery, ListResponse<List<ListItemSurfaceTypeDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetSurfaceTypesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemSurfaceTypeDto>>> Handle(GetSurfaceTypesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = SurfaceTypeFilters.SurfaceTypeNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<SurfaceType, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.SurfaceTypeSearchFilter();
        var filters = new Expression<Func<SurfaceType, bool>>[] { filter! };

        // Include related entities
        var includes = SurfaceTypesIncludes.GetSurfaceTypesIncludes();

        // Fetch paginated results
        var pagedSurfaceTypes = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedSurfaceTypes.Rows.Select(s => s.ListItemSurfaceTypeDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedSurfaceTypes.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemSurfaceTypeDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Surface Types fetched successfully." 
                : "No surface types found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedSurfaceTypes.UrlQuery.TotalRecords,
                PageSize = pagedSurfaceTypes.UrlQuery.PageSize,
                PageNumber = pagedSurfaceTypes.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
