// source
using server.src.Application.Geography.Natural.Timezones.Commands;
using server.src.Application.Geography.Natural.Timezones.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Natural.Timezones.Services;

public class TimezoneCommands : ITimezoneCommands
{
    private readonly RequestExecutor _requestExecutor;

    public TimezoneCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeTimezonesAsync(List<CreateTimezoneDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeTimezonesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeTimezonesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateTimezoneAsync(CreateTimezoneDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateTimezoneCommand(dto);
        return await _requestExecutor
            .Execute<CreateTimezoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateTimezoneAsync(Guid id, 
        UpdateTimezoneDto dto, CancellationToken token = default)
    {
        var command = new UpdateTimezoneCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateTimezoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateTimezoneAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateTimezoneCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateTimezoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateTimezoneAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateTimezoneCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateTimezoneCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteTimezoneAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteTimezoneCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteTimezoneCommand, Response<string>>(command, token);
    }
}