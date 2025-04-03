// source
using server.src.Application.Common.Services;
using server.src.Application.Weather.Collections.Observations.Commands;
using server.src.Application.Weather.Collections.Observations.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Observations.Dtos;

namespace server.src.Application.Weather.Collections.Observations.Services;

public class ObservationCommands : IObservationCommands
{
    private readonly RequestExecutor _requestExecutor;

    public ObservationCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeObservationsAsync(List<CreateObservationDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeObservationsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeObservationsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateObservationAsync(CreateObservationDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateObservationCommand(dto);
        return await _requestExecutor
            .Execute<CreateObservationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateObservationAsync(Guid id, UpdateObservationDto dto, 
        CancellationToken token = default)
    {
        var command = new UpdateObservationCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateObservationCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteObservationAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteObservationCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteObservationCommand, Response<string>>(command, token);
    }
}