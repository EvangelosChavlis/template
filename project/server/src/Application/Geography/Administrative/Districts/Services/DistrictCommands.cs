// source
using server.src.Application.Geography.Administrative.Districts.Commands;
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Districts.Services;

public class DistrictCommands : IDistrictCommands
{
    private readonly RequestExecutor _requestExecutor;

    public DistrictCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeDistrictsAsync(List<CreateDistrictDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeDistrictsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeDistrictsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateDistrictAsync(CreateDistrictDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateDistrictCommand(dto);
        return await _requestExecutor
            .Execute<CreateDistrictCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateDistrictAsync(Guid id, 
        UpdateDistrictDto dto, CancellationToken token = default)
    {
        var command = new UpdateDistrictCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateDistrictCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateDistrictAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateDistrictCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateDistrictCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateDistrictAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateDistrictCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateDistrictCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteDistrictAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteDistrictCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteDistrictCommand, Response<string>>(command, token);
    }
}