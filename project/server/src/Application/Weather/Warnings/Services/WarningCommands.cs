// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Application.Interfaces;
using server.src.Application.Weather.Warnings.Commands;
using server.src.Application.Weather.Warnings.Interfaces;

namespace server.src.Application.Weather.Warnings.Services;

public class WarningCommands : IWarningCommands
{
    private readonly IRequestHandler<InitializeWarningsCommand, Response<string>> _initializeWarningsHander;
    private readonly IRequestHandler<CreateWarningCommand, Response<string>> _createWarningHandler;
    private readonly IRequestHandler<UpdateWarningCommand, Response<string>> _updateWarningHandler;
    private readonly IRequestHandler<DeleteWarningCommand, Response<string>> _deleteWarningHandler;

    public WarningCommands(
        IRequestHandler<InitializeWarningsCommand, Response<string>> initializeWarningsHander,
        IRequestHandler<CreateWarningCommand, Response<string>> createWarningHandler,
        IRequestHandler<UpdateWarningCommand, Response<string>> updateWarningHandler,
        IRequestHandler<DeleteWarningCommand, Response<string>> deleteWarningHandler)
    {
        _initializeWarningsHander = initializeWarningsHander;
        _createWarningHandler = createWarningHandler;
        _updateWarningHandler = updateWarningHandler;
        _deleteWarningHandler = deleteWarningHandler;
    }

    public async Task<Response<string>> InitializeWarningsAsync(List<WarningDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeWarningsCommand(dto);
        return await _initializeWarningsHander.Handle(command, token);
    }

    public async Task<Response<string>> CreateWarningAsync(WarningDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateWarningCommand(dto);
        return await _createWarningHandler.Handle(command, token);
    }

    public async Task<Response<string>> UpdateWarningAsync(Guid id, WarningDto dto, 
        CancellationToken token = default)
    {
        var command = new UpdateWarningCommand(id, dto);
        return await _updateWarningHandler.Handle(command, token);
    }

    public async Task<Response<string>> DeleteWarningAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteWarningCommand(id, version);
        return await _deleteWarningHandler.Handle(command, token);
    }
}