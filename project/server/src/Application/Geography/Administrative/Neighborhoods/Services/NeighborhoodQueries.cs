// source
using server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Services;

public class NeighborhoodQueries : INeighborhoodQueries
{
    private readonly RequestExecutor _requestExecutor;

    public NeighborhoodQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemNeighborhoodDto>>> GetNeighborhoodsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetNeighborhoodsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetNeighborhoodsQuery, ListResponse<List<ListItemNeighborhoodDto>>>(query, token);
    }

    public async Task<Response<ItemNeighborhoodDto>> GetNeighborhoodByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetNeighborhoodByIdQuery(id);
        return await _requestExecutor
            .Execute<GetNeighborhoodByIdQuery, Response<ItemNeighborhoodDto>>(query, token);
    }
}