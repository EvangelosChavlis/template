// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Commands;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Weather.Forecasts.Services;

public class ForecastCommands : IForecastCommands
{
    private readonly IRequestHandler<InitializeForecastsCommand, Response<string>> _initializeForecastsHander;
    private readonly IRequestHandler<CreateForecastCommand, Response<string>> _createForecastHandler;
    private readonly IRequestHandler<UpdateForecastCommand, Response<string>> _updateForecastHandler;
    private readonly IRequestHandler<DeleteForecastCommand, Response<string>> _deleteForecastHandler;

    public ForecastCommands(
        IRequestHandler<InitializeForecastsCommand, Response<string>> initializeForecastsHander,
        IRequestHandler<CreateForecastCommand, Response<string>> createForecastHandler,
        IRequestHandler<UpdateForecastCommand, Response<string>> updateForecastHandler,
        IRequestHandler<DeleteForecastCommand, Response<string>> deleteForecastHandler)
    {
        _initializeForecastsHander = initializeForecastsHander;
        _createForecastHandler = createForecastHandler;
        _updateForecastHandler = updateForecastHandler;
        _deleteForecastHandler = deleteForecastHandler;
    }

    public async Task<Response<string>> InitializeForecastsAsync(List<CreateForecastDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeForecastsCommand(dto);
        return await _initializeForecastsHander.Handle(command, token);
    }

    public async Task<Response<string>> CreateForecastAsync(CreateForecastDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateForecastCommand(dto);
        return await _createForecastHandler.Handle(command, token);
    }

    public async Task<Response<string>> UpdateForecastAsync(Guid id, UpdateForecastDto dto, 
        CancellationToken token = default)
    {
        var command = new UpdateForecastCommand(id, dto);
        return await _updateForecastHandler.Handle(command, token);
    }

    public async Task<Response<string>> DeleteForecastAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteForecastCommand(id, version);
        return await _deleteForecastHandler.Handle(command, token);
    }
}