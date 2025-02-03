// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Warnings.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Weather.Warnings.Commands;

public record InitializeWarningsCommand(List<WarningDto> Dto) : IRequest<Response<string>>;

public class InitializeWarningsHandler : IRequestHandler<InitializeWarningsCommand, Response<string>>
{
    private readonly IWarningCommands _warningCommands;

    public InitializeWarningsHandler(IWarningCommands warningCommands)
    {
        _warningCommands = warningCommands;
    }

    public async Task<Response<string>> Handle(InitializeWarningsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _warningCommands.CreateWarningAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing warnings.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize warnings.");

        return new Response<string>()
            .WithMessage("Success initializing warnings.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
