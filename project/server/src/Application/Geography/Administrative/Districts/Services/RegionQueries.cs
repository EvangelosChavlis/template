// source
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Districts.Services;

public class DistrictQueries : IDistrictQueries
{
    private readonly RequestExecutor _requestExecutor;

    public DistrictQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemDistrictDto>>> GetDistrictsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetDistrictsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetDistrictsQuery, ListResponse<List<ListItemDistrictDto>>>(query, token);
    }

    public async Task<Response<ItemDistrictDto>> GetDistrictByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetDistrictByIdQuery(id);
        return await _requestExecutor
            .Execute<GetDistrictByIdQuery, Response<ItemDistrictDto>>(query, token);
    }
}