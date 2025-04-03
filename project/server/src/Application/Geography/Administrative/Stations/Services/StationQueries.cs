// source
using server.src.Application.Geography.Administrative.Stations.Interfaces;
using server.src.Application.Geography.Administrative.Stations.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Stations.Services;

public class StationQueries : IStationQueries
{
    private readonly RequestExecutor _requestExecutor;

    public StationQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemStationDto>>> GetStationsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetStationsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetStationsQuery, ListResponse<List<ListItemStationDto>>>(query, token);
    }

    public async Task<Response<ItemStationDto>> GetStationByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetStationByIdQuery(id);
        return await _requestExecutor
            .Execute<GetStationByIdQuery, Response<ItemStationDto>>(query, token);
    }
}