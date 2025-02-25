// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Filters;
using server.src.Application.Geography.Administrative.Neighborhoods.Includes;
using server.src.Application.Geography.Natural.Neighborhoods.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Queries;

public record GetNeighborhoodsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemNeighborhoodDto>>>;

public class GetNeighborhoodsHandler : IRequestHandler<GetNeighborhoodsQuery, ListResponse<List<ListItemNeighborhoodDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetNeighborhoodsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemNeighborhoodDto>>> Handle(GetNeighborhoodsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = NeighborhoodFilters.NeighborhoodNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Neighborhood, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.NeighborhoodSearchFilter();
        var filters = new Expression<Func<Neighborhood, bool>>[] { filter! };

        // Include related entities
        var includes = NeighborhoodsIncludes.GetNeighborhoodsIncludes();

        // Fetch paginated results
        var pagedNeighborhoods = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedNeighborhoods.Rows.Select(t => t.ListItemNeighborhoodDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedNeighborhoods.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemNeighborhoodDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Neighborhoods fetched successfully." 
                : "No neighborhoods found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedNeighborhoods.UrlQuery.TotalRecords,
                PageSize = pagedNeighborhoods.UrlQuery.PageSize,
                PageNumber = pagedNeighborhoods.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}