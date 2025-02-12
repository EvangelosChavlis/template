// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.Application.Metrics.Telemetry.Queries;
using server.src.Domain.Dto.Metrics;
using server.src.Application.Auth.Telemetry.Interfaces;

namespace server.src.Application.Telemetry.Services;

public class TelemetryQueries : ITelemetryQueries
{
    private readonly IRequestHandler<GetTelemetryQuery, ListResponse<List<ListItemTelemetryDto>>> _getTelemetryHandler;
    private readonly IRequestHandler<GetTelemetryByUserIdQuery, ListResponse<List<ListItemTelemetryDto>>> _getTelemetryByUserIdHandler;
    private readonly IRequestHandler<GetTelemetryByIdQuery, Response<ItemTelemetryDto>> _getTelemetryByIdHandler;

    public TelemetryQueries(
        IRequestHandler<GetTelemetryQuery, ListResponse<List<ListItemTelemetryDto>>> getTelemetryHandler,
        IRequestHandler<GetTelemetryByUserIdQuery, ListResponse<List<ListItemTelemetryDto>>> getTelemetryByUserIdHandler,
        IRequestHandler<GetTelemetryByIdQuery, Response<ItemTelemetryDto>> getTelemetryByIdHandler)
    {
        _getTelemetryHandler = getTelemetryHandler;
        _getTelemetryByUserIdHandler = getTelemetryByUserIdHandler;
        _getTelemetryByIdHandler = getTelemetryByIdHandler;
    }

    public async Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryAsync(UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetTelemetryQuery(urlQuery);
        return await _getTelemetryHandler.Handle(query, token);
    }

    public async Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryByUserIdAsync(Guid id, UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetTelemetryByUserIdQuery(id, urlQuery);
        return await _getTelemetryByUserIdHandler.Handle(query, token);
    }


    public async Task<Response<ItemTelemetryDto>> GetTelemetryByIdAsync(Guid id, CancellationToken token = default)
    {
        var query = new GetTelemetryByIdQuery(id);
        return await _getTelemetryByIdHandler.Handle(query, token);
    }
}