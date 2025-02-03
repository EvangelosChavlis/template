// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Warnings.Filters;
using server.src.Application.Weather.Warnings.Mappings;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Queries;

public record GetWarningsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemWarningDto>>>;

public class GetWarningsHandler : IRequestHandler<GetWarningsQuery, ListResponse<List<ListItemWarningDto>>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public GetWarningsHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemWarningDto>>> Handle(GetWarningsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = WarningFilters.WarningNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Warning, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.WarningSearchFilter();
        var filters = new Expression<Func<Warning, bool>>[] { filter! };

        // Include related entities
        var includes = WarningsIncludes.GetWarningsIncludes();

        // Fetch paginated results
        var pagedWarnings = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token);
        
        // Mapping
        var dto = pagedWarnings.Rows.Select(w => w.ListItemWarningDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedWarnings.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemWarningDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Warnings fetched successfully." 
                : "No warnings found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedWarnings.UrlQuery.TotalRecords,
                PageSize = pagedWarnings.UrlQuery.PageSize,
                PageNumber = pagedWarnings.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
