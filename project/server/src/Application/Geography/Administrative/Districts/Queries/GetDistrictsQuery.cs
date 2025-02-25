// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Filters;
using server.src.Application.Geography.Administrative.Districts.Includes;
using server.src.Application.Geography.Natural.Districts.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Districts.Queries;

public record GetDistrictsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemDistrictDto>>>;

public class GetDistrictsHandler : IRequestHandler<GetDistrictsQuery, ListResponse<List<ListItemDistrictDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetDistrictsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemDistrictDto>>> Handle(GetDistrictsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = DistrictFilters.DistrictNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<District, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.DistrictSearchFilter();
        var filters = new Expression<Func<District, bool>>[] { filter! };

        // Include related entities
        var includes = DistrictsIncludes.GetDistrictsIncludes();

        // Fetch paginated results
        var pagedDistricts = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedDistricts.Rows.Select(t => t.ListItemDistrictDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedDistricts.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemDistrictDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Districts fetched successfully." 
                : "No districts found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedDistricts.UrlQuery.TotalRecords,
                PageSize = pagedDistricts.UrlQuery.PageSize,
                PageNumber = pagedDistricts.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}