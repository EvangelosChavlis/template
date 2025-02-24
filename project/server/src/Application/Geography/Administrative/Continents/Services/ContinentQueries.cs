// source
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Continents.Services;

public class ContinentQueries : IContinentQueries
{
    private readonly RequestExecutor _requestExecutor;

    public ContinentQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemContinentDto>>> GetContinentsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetContinentsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetContinentsQuery, ListResponse<List<ListItemContinentDto>>>(query, token);
    }

    public async Task<Response<ItemContinentDto>> GetContinentByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetContinentByIdQuery(id);
        return await _requestExecutor
            .Execute<GetContinentByIdQuery, Response<ItemContinentDto>>(query, token);
    }
}