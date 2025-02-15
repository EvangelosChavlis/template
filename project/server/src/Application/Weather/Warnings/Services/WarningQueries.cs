// source
using server.src.Application.Weather.Warnings.Interfaces;
using server.src.Application.Weather.Warnings.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Weather.Warnings.Services;

public class WarningQueries : IWarningQueries
{
    private readonly RequestExecutor _requestExecutor;

    public WarningQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemWarningDto>>> GetWarningsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetWarningsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetWarningsQuery, ListResponse<List<ListItemWarningDto>>>(query, token);
    }

    public async Task<Response<List<PickerWarningDto>>> GetWarningsPickerAsync(CancellationToken token = default)
    {
        var query = new GetWarningsPickerQuery();
        return await _requestExecutor
            .Execute<GetWarningsPickerQuery, Response<List<PickerWarningDto>>>(query, token);
    }

    public async Task<Response<ItemWarningDto>> GetWarningByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetWarningByIdQuery(id);
        return await _requestExecutor
            .Execute<GetWarningByIdQuery, Response<ItemWarningDto>>(query, token);
    }
}