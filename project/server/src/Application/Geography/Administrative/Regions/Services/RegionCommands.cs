// source
using server.src.Application.Geography.Administrative.Regions.Commands;
using server.src.Application.Geography.Administrative.Regions.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Regions.Services;

public class RegionCommands : IRegionCommands
{
    private readonly RequestExecutor _requestExecutor;

    public RegionCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeRegionsAsync(List<CreateRegionDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeRegionsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeRegionsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateRegionAsync(CreateRegionDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateRegionCommand(dto);
        return await _requestExecutor
            .Execute<CreateRegionCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateRegionAsync(Guid id, 
        UpdateRegionDto dto, CancellationToken token = default)
    {
        var command = new UpdateRegionCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateRegionCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateRegionAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateRegionCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateRegionCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateRegionAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateRegionCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateRegionCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteRegionAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteRegionCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteRegionCommand, Response<string>>(command, token);
    }
}