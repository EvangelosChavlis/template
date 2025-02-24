// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Includes.States;
using server.src.Application.Geography.Administrative.States.Filters;
using server.src.Application.Geography.Natural.States.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.States.Queries;

public record GetStatesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemStateDto>>>;

public class GetStatesHandler : IRequestHandler<GetStatesQuery, ListResponse<List<ListItemStateDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetStatesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemStateDto>>> Handle(GetStatesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = StateFilters.StateNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<State, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.StateSearchFilter();
        var filters = new Expression<Func<State, bool>>[] { filter! };

        // Include related entities
        var includes = StatesIncludes.GetStatesIncludes();

        // Fetch paginated results
        var pagedStates = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedStates.Rows.Select(t => t.ListItemStateDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedStates.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemStateDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "States fetched successfully." 
                : "No states found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedStates.UrlQuery.TotalRecords,
                PageSize = pagedStates.UrlQuery.PageSize,
                PageNumber = pagedStates.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}