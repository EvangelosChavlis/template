// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Application.Geography.Administrative.Continents.Commands;

public record InitializeContinentsCommand(List<CreateContinentDto> Dto) : IRequest<Response<string>>;

public class InitializeContinentsHandler : IRequestHandler<InitializeContinentsCommand, Response<string>>
{
    private readonly IContinentCommands _continentCommands;

    public InitializeContinentsHandler(IContinentCommands continentCommands)
    {
        _continentCommands = continentCommands;
    }

    public async Task<Response<string>> Handle(InitializeContinentsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _continentCommands.CreateContinentAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing continent.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize continent.");

        return new Response<string>()
            .WithMessage("Success initializing continent.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
