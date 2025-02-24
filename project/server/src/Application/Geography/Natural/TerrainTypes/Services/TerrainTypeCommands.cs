// source
using server.src.Application.Geography.Natural.TerrainTypes.Commands;
using server.src.Application.Geography.Natural.TerrainTypes.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Natural.TerrainTypes.Services;

public class TerrainTypeCommands : ITerrainTypeCommands
{
    private readonly RequestExecutor _requestExecutor;

    public TerrainTypeCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeTerrainTypesAsync(List<CreateTerrainTypeDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeTerrainTypesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeTerrainTypesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateTerrainTypeAsync(CreateTerrainTypeDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateTerrainTypeCommand(dto);
        return await _requestExecutor
            .Execute<CreateTerrainTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateTerrainTypeAsync(Guid id, 
        UpdateTerrainTypeDto dto, CancellationToken token = default)
    {
        var command = new UpdateTerrainTypeCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateTerrainTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateTerrainTypeAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateTerrainTypeCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateTerrainTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateTerrainTypeAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateTerrainTypeCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateTerrainTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteTerrainTypeAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteTerrainTypeCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteTerrainTypeCommand, Response<string>>(command, token);
    }
}