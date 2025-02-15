// source
using server.src.Application.Weather.Warnings.Commands;
using server.src.Application.Weather.Warnings.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Weather.Warnings.Services;

public class WarningCommands : IWarningCommands
{
    private readonly RequestExecutor _requestExecutor;

    public WarningCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeWarningsAsync(List<CreateWarningDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeWarningsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeWarningsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateWarningAsync(CreateWarningDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateWarningCommand(dto);
        return await _requestExecutor
            .Execute<CreateWarningCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateWarningAsync(Guid id, 
        UpdateWarningDto dto, CancellationToken token = default)
    {
        var command = new UpdateWarningCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateWarningCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateWarningAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateWarningCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateWarningCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateWarningAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateWarningCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateWarningCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteWarningAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteWarningCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteWarningCommand, Response<string>>(command, token);
    }
}