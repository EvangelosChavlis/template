// source
using server.src.Application.Geography.Natural.Locations.Interfaces;
using server.src.Application.Geography.Natural.Locations.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Natural.Locations.Services;

public class LocationQueries : ILocationQueries
{
    private readonly RequestExecutor _requestExecutor;

    public LocationQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemLocationDto>>> GetLocationsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetLocationsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetLocationsQuery, ListResponse<List<ListItemLocationDto>>>(query, token);
    }

    public async Task<Response<ItemLocationDto>> GetLocationByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetLocationByIdQuery(id);
        return await _requestExecutor
            .Execute<GetLocationByIdQuery, Response<ItemLocationDto>>(query, token);
    }
}