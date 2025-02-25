// source
using server.src.Application.Geography.Administrative.Regions.Interfaces;
using server.src.Application.Geography.Administrative.Regions.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Regions.Services;

public class RegionQueries : IRegionQueries
{
    private readonly RequestExecutor _requestExecutor;

    public RegionQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemRegionDto>>> GetRegionsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetRegionsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetRegionsQuery, ListResponse<List<ListItemRegionDto>>>(query, token);
    }

    public async Task<Response<ItemRegionDto>> GetRegionByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetRegionByIdQuery(id);
        return await _requestExecutor
            .Execute<GetRegionByIdQuery, Response<ItemRegionDto>>(query, token);
    }
}