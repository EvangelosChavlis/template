// source
using server.src.Application.Weather.MoonPhases.Commands;
using server.src.Application.Weather.MoonPhases.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Weather.MoonPhases.Services;

public class MoonPhaseCommands : IMoonPhaseCommands
{
    private readonly RequestExecutor _requestExecutor;

    public MoonPhaseCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeMoonPhasesAsync(List<CreateMoonPhaseDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeMoonPhasesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeMoonPhasesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateMoonPhaseAsync(CreateMoonPhaseDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateMoonPhaseCommand(dto);
        return await _requestExecutor
            .Execute<CreateMoonPhaseCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateMoonPhaseAsync(Guid id, 
        UpdateMoonPhaseDto dto, CancellationToken token = default)
    {
        var command = new UpdateMoonPhaseCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateMoonPhaseCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateMoonPhaseAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateMoonPhaseCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateMoonPhaseCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateMoonPhaseAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateMoonPhaseCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateMoonPhaseCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteMoonPhaseAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteMoonPhaseCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteMoonPhaseCommand, Response<string>>(command, token);
    }
}