// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.MoonPhases.Filters;
using server.src.Application.Weather.MoonPhases.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.MoonPhases.Queries;

public record GetMoonPhasesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemMoonPhaseDto>>>;

public class GetMoonPhasesHandler : IRequestHandler<GetMoonPhasesQuery, ListResponse<List<ListItemMoonPhaseDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetMoonPhasesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemMoonPhaseDto>>> Handle(GetMoonPhasesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = MoonPhaseFilters.MoonPhaseNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<MoonPhase, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.MoonPhaseSearchFilter();
        var filters = new Expression<Func<MoonPhase, bool>>[] { filter! };

        // Include related entities
        var includes = MoonPhasesIncludes.GetMoonPhasesIncludes();

        // Fetch paginated results
        var pagedMoonPhases = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedMoonPhases.Rows.Select(m => m.ListItemMoonPhaseDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedMoonPhases.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemMoonPhaseDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Moon phases fetched successfully." 
                : "No moon phases found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedMoonPhases.UrlQuery.TotalRecords,
                PageSize = pagedMoonPhases.UrlQuery.PageSize,
                PageNumber = pagedMoonPhases.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
