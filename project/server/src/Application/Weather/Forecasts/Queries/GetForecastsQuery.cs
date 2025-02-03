// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Filters;
using server.src.Application.Weather.Forecasts.Mappings;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Forecasts.Queries;

public record GetForecastsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemForecastDto>>>;

public class GetForecastsHandler : IRequestHandler<GetForecastsQuery, ListResponse<List<ListItemForecastDto>>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public GetForecastsHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemForecastDto>>> Handle(GetForecastsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;
        
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ForecastFilters.ForecastTempSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Forecast, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ForecastSearchFilter();

        var filters = new Expression<Func<Forecast, bool>>[] { filter! };

        // Including
        var includes = ForecastsIncludes.GetForecastsIncludes();

        // Paging
        var pagedForecasts = await _commonRepository.GetPagedResultsAsync(pageParams, filters, includes, token);
        // Mapping
        var dto = pagedForecasts.Rows.Select(w => w.ListItemForecastDtoMapping()).ToList();

        // Determine success 
        var success = pagedForecasts.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemForecastDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Forecasts fetched successfully." : 
                "No forecasts found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedForecasts.UrlQuery.TotalRecords,
                PageSize = pagedForecasts.UrlQuery.PageSize,
                PageNumber = pagedForecasts.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
