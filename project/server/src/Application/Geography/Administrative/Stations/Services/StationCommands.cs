// source
using server.src.Application.Geography.Administrative.Stations.Commands;
using server.src.Application.Geography.Administrative.Stations.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Stations.Services;

public class StationCommands : IStationCommands
{
    private readonly RequestExecutor _requestExecutor;

    public StationCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeStationsAsync(List<CreateStationDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeStationsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeStationsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateStationAsync(CreateStationDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateStationCommand(dto);
        return await _requestExecutor
            .Execute<CreateStationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateStationAsync(Guid id, 
        UpdateStationDto dto, CancellationToken token = default)
    {
        var command = new UpdateStationCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateStationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateStationAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateStationCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateStationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateStationAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateStationCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateStationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteStationAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteStationCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteStationCommand, Response<string>>(command, token);
    }
}