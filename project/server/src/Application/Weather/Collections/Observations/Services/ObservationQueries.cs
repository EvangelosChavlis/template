// source
using server.src.Application.Common.Services;
using server.src.Application.Weather.Collections.Observations.Interfaces;
using server.src.Application.Weather.Collections.Observations.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Observations.Dtos;

namespace server.src.Application.Weather.Collections.Observations.Services;

public class ObservationQueries : IObservationQueries
{
    private readonly RequestExecutor _requestExecutor;

    public ObservationQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<List<StatItemObservationDto>>> GetObservationsStatsAsync(CancellationToken token = default)
    {
        var query = new GetObservationsStatsQuery();
        return await _requestExecutor
            .Execute<GetObservationsStatsQuery, Response<List<StatItemObservationDto>>>(query, token);
    }

    public async Task<ListResponse<List<ListItemObservationDto>>> GetObservationsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetObservationsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetObservationsQuery, ListResponse<List<ListItemObservationDto>>>(query, token);
    }

    public async Task<Response<ItemObservationDto>> GetObservationByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetObservationByIdQuery(id);
        return await _requestExecutor
            .Execute<GetObservationByIdQuery, Response<ItemObservationDto>>(query, token);
    }
}