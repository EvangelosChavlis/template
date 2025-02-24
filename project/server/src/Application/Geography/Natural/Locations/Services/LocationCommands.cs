// source
using server.src.Application.Geography.Natural.Locations.Commands;
using server.src.Application.Geography.Natural.Locations.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Natural.Locations.Services;

public class LocationCommands : ILocationCommands
{
    private readonly RequestExecutor _requestExecutor;

    public LocationCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeLocationsAsync(List<CreateLocationDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeLocationsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeLocationsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateLocationAsync(CreateLocationDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateLocationCommand(dto);
        return await _requestExecutor
            .Execute<CreateLocationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateLocationAsync(Guid id, 
        UpdateLocationDto dto, CancellationToken token = default)
    {
        var command = new UpdateLocationCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateLocationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateLocationAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateLocationCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateLocationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateLocationAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateLocationCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateLocationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteLocationAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteLocationCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteLocationCommand, Response<string>>(command, token);
    }
}