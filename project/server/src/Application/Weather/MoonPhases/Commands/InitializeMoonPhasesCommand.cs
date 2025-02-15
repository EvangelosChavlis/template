// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.MoonPhases.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;

namespace server.src.Application.Weather.MoonPhases.Commands;

public record InitializeMoonPhasesCommand(List<CreateMoonPhaseDto> Dto) : IRequest<Response<string>>;

public class InitializeMoonPhasesHandler : IRequestHandler<InitializeMoonPhasesCommand, Response<string>>
{
    private readonly IMoonPhaseCommands _moonPhaseCommands;

    public InitializeMoonPhasesHandler(IMoonPhaseCommands moonPhaseCommands)
    {
        _moonPhaseCommands = moonPhaseCommands;
    }

    public async Task<Response<string>> Handle(InitializeMoonPhasesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _moonPhaseCommands.CreateMoonPhaseAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing moon phases.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize moon phases.");

        return new Response<string>()
            .WithMessage("Success initializing moon phases.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
