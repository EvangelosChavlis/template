// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Forecasts.Filters;
using server.src.Application.Weather.Collections.Forecasts.Mappings;
using server.src.Application.Weather.Collections.Forecasts.Projections;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Forecasts.Dtos;
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.Forecasts.Queries;

public record GetForecastsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemForecastDto>>>;

public class GetForecastsHandler : IRequestHandler<GetForecastsQuery, ListResponse<List<ListItemForecastDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetForecastsHandler(ICommonRepository commonRepository)
    {
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
        // Projection
        var projection = ForecastProjections.GetForecastsProjection();
        // Paging
        var pagedForecasts = await _commonRepository.GetPagedResultsAsync(
            pageParams, 
            filters, 
            includes, 
            token: token
        );
        // Mapping
        var dto = pagedForecasts.Rows.Select(f => f.ListItemForecastDtoMapping()).ToList();

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
