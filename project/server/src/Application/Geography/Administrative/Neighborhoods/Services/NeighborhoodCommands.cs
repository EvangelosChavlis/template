// source
using server.src.Application.Geography.Administrative.Neighborhoods.Commands;
using server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Services;

public class NeighborhoodCommands : INeighborhoodCommands
{
    private readonly RequestExecutor _requestExecutor;

    public NeighborhoodCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeNeighborhoodsAsync(List<CreateNeighborhoodDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeNeighborhoodsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeNeighborhoodsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateNeighborhoodAsync(CreateNeighborhoodDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateNeighborhoodCommand(dto);
        return await _requestExecutor
            .Execute<CreateNeighborhoodCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateNeighborhoodAsync(Guid id, 
        UpdateNeighborhoodDto dto, CancellationToken token = default)
    {
        var command = new UpdateNeighborhoodCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateNeighborhoodCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateNeighborhoodAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateNeighborhoodCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateNeighborhoodCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateNeighborhoodAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateNeighborhoodCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateNeighborhoodCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteNeighborhoodAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteNeighborhoodCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteNeighborhoodCommand, Response<string>>(command, token);
    }
}