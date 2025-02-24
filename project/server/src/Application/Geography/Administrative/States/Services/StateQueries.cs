// source
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Application.Geography.Administrative.States.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.States.Services;

public class StateQueries : IStateQueries
{
    private readonly RequestExecutor _requestExecutor;

    public StateQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemStateDto>>> GetStatesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetStatesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetStatesQuery, ListResponse<List<ListItemStateDto>>>(query, token);
    }

    public async Task<Response<ItemStateDto>> GetStateByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetStateByIdQuery(id);
        return await _requestExecutor
            .Execute<GetStateByIdQuery, Response<ItemStateDto>>(query, token);
    }
}