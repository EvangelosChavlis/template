// source
using server.src.Application.Geography.Natural.ClimateZones.Interfaces;
using server.src.Application.Geography.Natural.ClimateZones.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Services;

public class ClimateZoneQueries : IClimateZoneQueries
{
    private readonly RequestExecutor _requestExecutor;

    public ClimateZoneQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemClimateZoneDto>>> GetClimateZonesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetClimateZonesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetClimateZonesQuery, ListResponse<List<ListItemClimateZoneDto>>>(query, token);
    }

    public async Task<Response<List<PickerClimateZoneDto>>> GetClimateZonesPickerAsync(CancellationToken token = default)
    {
        var query = new GetClimateZonesPickerQuery();
        return await _requestExecutor
            .Execute<GetClimateZonesPickerQuery, Response<List<PickerClimateZoneDto>>>(query, token);
    }

    public async Task<Response<ItemClimateZoneDto>> GetClimateZoneByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetClimateZoneByIdQuery(id);
        return await _requestExecutor
            .Execute<GetClimateZoneByIdQuery, Response<ItemClimateZoneDto>>(query, token);
    }
}