// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Regions.Filters;
using server.src.Application.Geography.Administrative.Regions.Includes;
using server.src.Application.Geography.Natural.Regions.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Regions.Queries;

public record GetRegionsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemRegionDto>>>;

public class GetRegionsHandler : IRequestHandler<GetRegionsQuery, ListResponse<List<ListItemRegionDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetRegionsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemRegionDto>>> Handle(GetRegionsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = RegionFilters.RegionNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Region, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.RegionSearchFilter();
        var filters = new Expression<Func<Region, bool>>[] { filter! };

        // Include related entities
        var includes = RegionsIncludes.GetRegionsIncludes();

        // Fetch paginated results
        var pagedRegions = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedRegions.Rows.Select(t => t.ListItemRegionDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedRegions.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemRegionDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Regions fetched successfully." 
                : "No regions found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedRegions.UrlQuery.TotalRecords,
                PageSize = pagedRegions.UrlQuery.PageSize,
                PageNumber = pagedRegions.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}