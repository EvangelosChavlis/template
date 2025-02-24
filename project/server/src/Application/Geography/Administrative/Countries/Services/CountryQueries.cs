// source
using server.src.Application.Geography.Administrative.Countries.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Administrative.Countries.Services;

public class CountryQueries : ICountryQueries
{
    private readonly RequestExecutor _requestExecutor;

    public CountryQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemCountryDto>>> GetCountriesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetCountriesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetCountriesQuery, ListResponse<List<ListItemCountryDto>>>(query, token);
    }

    public async Task<Response<ItemCountryDto>> GetCountryByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetCountryByIdQuery(id);
        return await _requestExecutor
            .Execute<GetCountryByIdQuery, Response<ItemCountryDto>>(query, token);
    }
}