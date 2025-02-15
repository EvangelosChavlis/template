// source
using server.src.Application.Metrics.Telemetry.Queries;
using server.src.Application.Auth.TelemetryRecords.Interfaces;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;
using server.src.Domain.Common.Models;
using server.src.Application.Metrics.TelemetryRecords.Queries;

namespace server.src.Application.TelemetryRecords.Services;

public class TelemetryRecordQueries : ITelemetryRecordQueries
{
    private readonly RequestExecutor _requestExecutor;

    public TelemetryRecordQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemTelemetryRecordDto>>> GetTelemetryRecordsAsync(
        UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetTelemetryRecordsQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetTelemetryRecordsQuery, ListResponse<List<ListItemTelemetryRecordDto>>>(query, token);
    }

    public async Task<ListResponse<List<ListItemTelemetryRecordDto>>> GetTelemetryRecordByUserIdAsync(
        Guid id, UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetTelemetryRecordByUserIdQuery(id, urlQuery);
        return await _requestExecutor
            .Execute<GetTelemetryRecordByUserIdQuery, ListResponse<List<ListItemTelemetryRecordDto>>>(query, token);
    }


    public async Task<Response<ItemTelemetryRecordDto>> GetTelemetryRecordByIdAsync(
        Guid id, CancellationToken token = default)
    {
        var query = new GetTelemetryRecordByIdQuery(id);
        return await _requestExecutor
            .Execute<GetTelemetryRecordByIdQuery, Response<ItemTelemetryRecordDto>>(query, token);
    }
}