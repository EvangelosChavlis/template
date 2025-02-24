// source
using server.src.Application.Geography.Administrative.Continents.Commands;
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Continents.Services;

public class ContinentCommands : IContinentCommands
{
    private readonly RequestExecutor _requestExecutor;

    public ContinentCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeContinentsAsync(List<CreateContinentDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeContinentsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeContinentsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateContinentAsync(CreateContinentDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateContinentCommand(dto);
        return await _requestExecutor
            .Execute<CreateContinentCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateContinentAsync(Guid id, 
        UpdateContinentDto dto, CancellationToken token = default)
    {
        var command = new UpdateContinentCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateContinentCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateContinentAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateContinentCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateContinentCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateContinentAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateContinentCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateContinentCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteContinentAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteContinentCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteContinentCommand, Response<string>>(command, token);
    }
}