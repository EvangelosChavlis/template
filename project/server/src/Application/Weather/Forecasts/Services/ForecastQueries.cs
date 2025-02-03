// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Application.Weather.Forecasts.Queries;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;

namespace server.src.Application.Weather.Forecasts.Services;

public class ForecastQueries : IForecastQueries
{
    private readonly IRequestHandler<GetForecastsStatsQuery, Response<List<StatItemForecastDto>>> _getForecastsStatsHandler;
    private readonly IRequestHandler<GetForecastsQuery, ListResponse<List<ListItemForecastDto>>> _getForecastsHandler;
    private readonly IRequestHandler<GetForecastByIdQuery, Response<ItemForecastDto>> _getForecastByIdHandler;

    public ForecastQueries(
        IRequestHandler<GetForecastsStatsQuery, Response<List<StatItemForecastDto>>> getForecastsStatsHandler,
        IRequestHandler<GetForecastsQuery, ListResponse<List<ListItemForecastDto>>> getForecastsHandler,
        IRequestHandler<GetForecastByIdQuery, Response<ItemForecastDto>> getForecastByIdHandler)
    {
        _getForecastsStatsHandler = getForecastsStatsHandler;
        _getForecastsHandler = getForecastsHandler;
        _getForecastByIdHandler = getForecastByIdHandler;
    }

    public async Task<Response<List<StatItemForecastDto>>> GetForecastsStatsAsync(CancellationToken token = default)
    {
        var query = new GetForecastsStatsQuery();
        return await _getForecastsStatsHandler.Handle(query, token);
    }

    public async Task<ListResponse<List<ListItemForecastDto>>> GetForecastsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetForecastsQuery(urlQuery);
        return await _getForecastsHandler.Handle(query, token);
    }

    public async Task<Response<ItemForecastDto>> GetForecastByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetForecastByIdQuery(id);
        return await _getForecastByIdHandler.Handle(query, token);
    }
}