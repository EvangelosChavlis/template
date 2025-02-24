// source
using server.src.Application.Geography.Natural.Timezones.Interfaces;
using server.src.Application.Geography.Natural.Timezones.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Natural.Timezones.Services;

public class TimezoneQueries : ITimezoneQueries
{
    private readonly RequestExecutor _requestExecutor;

    public TimezoneQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemTimezoneDto>>> GetTimezonesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetTimezonesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetTimezonesQuery, ListResponse<List<ListItemTimezoneDto>>>(query, token);
    }

    public async Task<Response<List<PickerTimezoneDto>>> GetTimezonesPickerAsync(CancellationToken token = default)
    {
        var query = new GetTimezonesPickerQuery();
        return await _requestExecutor
            .Execute<GetTimezonesPickerQuery, Response<List<PickerTimezoneDto>>>(query, token);
    }

    public async Task<Response<ItemTimezoneDto>> GetTimezoneByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetTimezoneByIdQuery(id);
        return await _requestExecutor
            .Execute<GetTimezoneByIdQuery, Response<ItemTimezoneDto>>(query, token);
    }
}