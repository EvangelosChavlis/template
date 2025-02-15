// source
using server.src.Application.Common.Services;
using server.src.Application.Weather.Forecasts.Commands;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Forecasts.Dtos;

namespace server.src.Application.Weather.Forecasts.Services;

public class ForecastCommands : IForecastCommands
{
    private readonly RequestExecutor _requestExecutor;

    public ForecastCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeForecastsAsync(List<CreateForecastDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeForecastsCommand(dto);
        return await _requestExecutor
            .Execute<InitializeForecastsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateForecastAsync(CreateForecastDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateForecastCommand(dto);
        return await _requestExecutor
            .Execute<CreateForecastCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateForecastAsync(Guid id, UpdateForecastDto dto, 
        CancellationToken token = default)
    {
        var command = new UpdateForecastCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateForecastCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteForecastAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteForecastCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteForecastCommand, Response<string>>(command, token);
    }
}