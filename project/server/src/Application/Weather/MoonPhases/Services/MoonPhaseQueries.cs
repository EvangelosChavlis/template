// source
using server.src.Application.Weather.MoonPhases.Interfaces;
using server.src.Application.Weather.MoonPhases.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Weather.MoonPhases.Services;

public class MoonPhaseQueries : IMoonPhaseQueries
{
    private readonly RequestExecutor _requestExecutor;

    public MoonPhaseQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemMoonPhaseDto>>> GetMoonPhasesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetMoonPhasesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetMoonPhasesQuery, ListResponse<List<ListItemMoonPhaseDto>>>(query, token);
    }

    public async Task<Response<List<PickerMoonPhaseDto>>> GetMoonPhasesPickerAsync(CancellationToken token = default)
    {
        var query = new GetMoonPhasesPickerQuery();
        return await _requestExecutor
            .Execute<GetMoonPhasesPickerQuery, Response<List<PickerMoonPhaseDto>>>(query, token);
    }

    public async Task<Response<ItemMoonPhaseDto>> GetMoonPhaseByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetMoonPhaseByIdQuery(id);
        return await _requestExecutor
            .Execute<GetMoonPhaseByIdQuery, Response<ItemMoonPhaseDto>>(query, token);
    }
}