// source
using server.src.Application.Common.Services;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Application.Weather.Forecasts.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Forecasts.Dtos;

namespace server.src.Application.Weather.Forecasts.Services;

public class ForecastQueries : IForecastQueries
{
    private readonly RequestExecutor _requestExecutor;

    public ForecastQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<List<StatItemForecastDto>>> GetForecastsStatsAsync(CancellationToken token = default)
    {
        var query = new GetForecastsStatsQuery();
        return await _requestExecutor
            .Execute<GetForecastsStatsQuery, Response<List<StatItemForecastDto>>>(query, token);
    }

    public async Task<ListResponse<List<ListItemForecastDto>>> GetForecastsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetForecastsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetForecastsQuery, ListResponse<List<ListItemForecastDto>>>(query, token);
    }

    public async Task<Response<ItemForecastDto>> GetForecastByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetForecastByIdQuery(id);
        return await _requestExecutor
            .Execute<GetForecastByIdQuery, Response<ItemForecastDto>>(query, token);
    }
}