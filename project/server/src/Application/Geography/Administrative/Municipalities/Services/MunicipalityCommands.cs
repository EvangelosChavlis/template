// source
using server.src.Application.Geography.Administrative.Municipalities.Commands;
using server.src.Application.Geography.Administrative.Municipalities.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Municipalities.Services;

public class MunicipalityCommands : IMunicipalityCommands
{
    private readonly RequestExecutor _requestExecutor;

    public MunicipalityCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeMunicipalitiesAsync(List<CreateMunicipalityDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeMunicipalitiesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeMunicipalitiesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateMunicipalityAsync(CreateMunicipalityDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateMunicipalityCommand(dto);
        return await _requestExecutor
            .Execute<CreateMunicipalityCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateMunicipalityAsync(Guid id, 
        UpdateMunicipalityDto dto, CancellationToken token = default)
    {
        var command = new UpdateMunicipalityCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateMunicipalityCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateMunicipalityAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateMunicipalityCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateMunicipalityCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateMunicipalityAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateMunicipalityCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateMunicipalityCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteMunicipalityAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteMunicipalityCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteMunicipalityCommand, Response<string>>(command, token);
    }
}