// source
using server.src.Application.Geography.Natural.NaturalFeatures.Commands;
using server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Services;

public class NaturalFeatureCommands : INaturalFeatureCommands
{
    private readonly RequestExecutor _requestExecutor;

    public NaturalFeatureCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeNaturalFeaturesAsync(List<CreateNaturalFeatureDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeNaturalFeaturesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeNaturalFeaturesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateNaturalFeatureAsync(CreateNaturalFeatureDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateNaturalFeatureCommand(dto);
        return await _requestExecutor
            .Execute<CreateNaturalFeatureCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateNaturalFeatureAsync(Guid id, 
        UpdateNaturalFeatureDto dto, CancellationToken token = default)
    {
        var command = new UpdateNaturalFeatureCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateNaturalFeatureCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateNaturalFeatureAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateNaturalFeatureCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateNaturalFeatureCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateNaturalFeatureAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateNaturalFeatureCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateNaturalFeatureCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteNaturalFeatureAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteNaturalFeatureCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteNaturalFeatureCommand, Response<string>>(command, token);
    }
}