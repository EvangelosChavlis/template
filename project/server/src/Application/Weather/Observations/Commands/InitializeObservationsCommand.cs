// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Observations.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Observations.Dtos;

namespace server.src.Application.Weather.Observations.Commands;

public record InitializeObservationsCommand(
    List<CreateObservationDto> Dto
) : IRequest<Response<string>>;

public class InitializeObservationsHandler : IRequestHandler<InitializeObservationsCommand, Response<string>>
{
    private readonly IObservationCommands _observationCommands;
    
    public InitializeObservationsHandler(IObservationCommands observationCommands)
    {
        _observationCommands = observationCommands;
    }

    public async Task<Response<string>> Handle(InitializeObservationsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();
        foreach (var item in command.Dto)
        {
            var result = await _observationCommands.CreateObservationAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        // Saving failed.
        if(!success)
            return new Response<string>()
                .WithMessage("Error initializing observations.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize observations.");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing observations.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}