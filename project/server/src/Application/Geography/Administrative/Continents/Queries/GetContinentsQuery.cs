// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Filters;
using server.src.Application.Geography.Natural.Continents.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Includes;

namespace server.src.Application.Geography.Administrative.Continents.Queries;

public record GetContinentsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemContinentDto>>>;

public class GetContinentsHandler : IRequestHandler<GetContinentsQuery, ListResponse<List<ListItemContinentDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetContinentsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemContinentDto>>> Handle(GetContinentsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ContinentFilters.ContinentNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Continent, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ContinentSearchFilter();
        var filters = new Expression<Func<Continent, bool>>[] { filter! };

        // Include related entities
        var includes = ContinentsIncludes.GetContinentsIncludes();

        // Fetch paginated results
        var pagedContinents = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedContinents.Rows.Select(t => t.ListItemContinentDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedContinents.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemContinentDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Continents fetched successfully." 
                : "No continents found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedContinents.UrlQuery.TotalRecords,
                PageSize = pagedContinents.UrlQuery.PageSize,
                PageNumber = pagedContinents.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}