// source
using server.src.Application.Geography.Administrative.States.Commands;
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.States.Services;

public class StateCommands : IStateCommands
{
    private readonly RequestExecutor _requestExecutor;

    public StateCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeStatesAsync(List<CreateStateDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeStatesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeStatesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateStateAsync(CreateStateDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateStateCommand(dto);
        return await _requestExecutor
            .Execute<CreateStateCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateStateAsync(Guid id, 
        UpdateStateDto dto, CancellationToken token = default)
    {
        var command = new UpdateStateCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateStateCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateStateAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateStateCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateStateCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateStateAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateStateCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateStateCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteStateAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteStateCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteStateCommand, Response<string>>(command, token);
    }
}