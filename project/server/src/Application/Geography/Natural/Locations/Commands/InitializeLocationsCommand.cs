// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Locations.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Application.Geography.Natural.Locations.Commands;

public record InitializeLocationsCommand(List<CreateLocationDto> Dto) : IRequest<Response<string>>;

public class InitializeLocationsHandler : IRequestHandler<InitializeLocationsCommand, Response<string>>
{
    private readonly ILocationCommands _locationCommands;

    public InitializeLocationsHandler(ILocationCommands locationCommands)
    {
        _locationCommands = locationCommands;
    }

    public async Task<Response<string>> Handle(InitializeLocationsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _locationCommands.CreateLocationAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing locations.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize location.");

        return new Response<string>()
            .WithMessage("Success initializing locations.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
