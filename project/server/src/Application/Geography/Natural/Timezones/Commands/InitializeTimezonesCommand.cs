// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Timezones.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;

namespace server.src.Application.Geography.Natural.Timezones.Commands;

public record InitializeTimezonesCommand(List<CreateTimezoneDto> Dto) : IRequest<Response<string>>;

public class InitializeTimezonesHandler : IRequestHandler<InitializeTimezonesCommand, Response<string>>
{
    private readonly ITimezoneCommands _timezoneCommands;

    public InitializeTimezonesHandler(ITimezoneCommands timezoneCommands)
    {
        _timezoneCommands = timezoneCommands;
    }

    public async Task<Response<string>> Handle(InitializeTimezonesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _timezoneCommands.CreateTimezoneAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing timezone.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize timezone.");

        return new Response<string>()
            .WithMessage("Success initializing timezone.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
