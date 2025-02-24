// source
using server.src.Application.Geography.Natural.TerrainTypes.Interfaces;
using server.src.Application.Geography.Natural.TerrainTypes.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Services;

public class TerrainTypeQueries : ITerrainTypeQueries
{
    private readonly RequestExecutor _requestExecutor;

    public TerrainTypeQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemTerrainTypeDto>>> GetTerrainTypesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetTerrainTypesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetTerrainTypesQuery, ListResponse<List<ListItemTerrainTypeDto>>>(query, token);
    }

    public async Task<Response<List<PickerTerrainTypeDto>>> GetTerrainTypesPickerAsync(CancellationToken token = default)
    {
        var query = new GetTerrainTypesPickerQuery();
        return await _requestExecutor
            .Execute<GetTerrainTypesPickerQuery, Response<List<PickerTerrainTypeDto>>>(query, token);
    }

    public async Task<Response<ItemTerrainTypeDto>> GetTerrainTypeByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetTerrainTypeByIdQuery(id);
        return await _requestExecutor
            .Execute<GetTerrainTypeByIdQuery, Response<ItemTerrainTypeDto>>(query, token);
    }
}