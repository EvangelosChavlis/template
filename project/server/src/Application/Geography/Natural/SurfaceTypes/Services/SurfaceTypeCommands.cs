// source
using server.src.Application.Geography.Natural.SurfaceTypes.Commands;
using server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Services;

public class SurfaceTypeCommands : ISurfaceTypeCommands
{
    private readonly RequestExecutor _requestExecutor;

    public SurfaceTypeCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeSurfaceTypesAsync(List<CreateSurfaceTypeDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeSurfaceTypesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeSurfaceTypesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateSurfaceTypeAsync(CreateSurfaceTypeDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateSurfaceTypeCommand(dto);
        return await _requestExecutor
            .Execute<CreateSurfaceTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateSurfaceTypeAsync(Guid id, 
        UpdateSurfaceTypeDto dto, CancellationToken token = default)
    {
        var command = new UpdateSurfaceTypeCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateSurfaceTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateSurfaceTypeAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateSurfaceTypeCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateSurfaceTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateSurfaceTypeAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateSurfaceTypeCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateSurfaceTypeCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteSurfaceTypeAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteSurfaceTypeCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteSurfaceTypeCommand, Response<string>>(command, token);
    }
}