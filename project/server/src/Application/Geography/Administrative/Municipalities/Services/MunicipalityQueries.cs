// source
using server.src.Application.Geography.Administrative.Municipalities.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Services;

public class MunicipalityQueries : IMunicipalityQueries
{
    private readonly RequestExecutor _requestExecutor;

    public MunicipalityQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemMunicipalityDto>>> GetMunicipalitiesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetMunicipalitiesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetMunicipalitiesQuery, ListResponse<List<ListItemMunicipalityDto>>>(query, token);
    }

    public async Task<Response<ItemMunicipalityDto>> GetMunicipalityByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetMunicipalityByIdQuery(id);
        return await _requestExecutor
            .Execute<GetMunicipalityByIdQuery, Response<ItemMunicipalityDto>>(query, token);
    }
}