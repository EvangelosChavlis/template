// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Interfaces;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Weather.Forecasts.Commands;

public record InitializeForecastsCommand(List<CreateForecastDto> Dto) : IRequest<Response<string>>;

public class InitializeForecastsHandler : IRequestHandler<InitializeForecastsCommand, Response<string>>
{
    private readonly IForecastCommands _forecastCommands;
    
    public InitializeForecastsHandler(IForecastCommands forecastCommands)
    {
        _forecastCommands = forecastCommands;
    }

    public async Task<Response<string>> Handle(InitializeForecastsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();
        foreach (var item in command.Dto)
        {
            var result = await _forecastCommands.CreateForecastAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        // Saving failed.
        if(!success)
            return new Response<string>()
                .WithMessage("Error initializing forecasts.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize forecasts.");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing forecasts.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}