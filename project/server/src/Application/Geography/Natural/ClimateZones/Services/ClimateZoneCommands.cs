// source
using server.src.Application.Geography.Natural.ClimateZones.Commands;
using server.src.Application.Geography.Natural.ClimateZones.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Natural.ClimateZones.Services;

public class ClimateZoneCommands : IClimateZoneCommands
{
    private readonly RequestExecutor _requestExecutor;

    public ClimateZoneCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeClimateZonesAsync(List<CreateClimateZoneDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeClimateZonesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeClimateZonesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateClimateZoneAsync(CreateClimateZoneDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateClimateZoneCommand(dto);
        return await _requestExecutor
            .Execute<CreateClimateZoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateClimateZoneAsync(Guid id, 
        UpdateClimateZoneDto dto, CancellationToken token = default)
    {
        var command = new UpdateClimateZoneCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateClimateZoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateClimateZoneAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateClimateZoneCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateClimateZoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateClimateZoneAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateClimateZoneCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateClimateZoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteClimateZoneAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteClimateZoneCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteClimateZoneCommand, Response<string>>(command, token);
    }
}