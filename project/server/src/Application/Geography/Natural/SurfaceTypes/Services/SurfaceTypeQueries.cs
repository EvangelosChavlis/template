// source
using server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;
using server.src.Application.Geography.Natural.SurfaceTypes.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Services;

public class SurfaceTypeQueries : ISurfaceTypeQueries
{
    private readonly RequestExecutor _requestExecutor;

    public SurfaceTypeQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemSurfaceTypeDto>>> GetSurfaceTypesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetSurfaceTypesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetSurfaceTypesQuery, ListResponse<List<ListItemSurfaceTypeDto>>>(query, token);
    }

    public async Task<Response<List<PickerSurfaceTypeDto>>> GetSurfaceTypesPickerAsync(CancellationToken token = default)
    {
        var query = new GetSurfaceTypesPickerQuery();
        return await _requestExecutor
            .Execute<GetSurfaceTypesPickerQuery, Response<List<PickerSurfaceTypeDto>>>(query, token);
    }

    public async Task<Response<ItemSurfaceTypeDto>> GetSurfaceTypeByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetSurfaceTypeByIdQuery(id);
        return await _requestExecutor
            .Execute<GetSurfaceTypeByIdQuery, Response<ItemSurfaceTypeDto>>(query, token);
    }
}