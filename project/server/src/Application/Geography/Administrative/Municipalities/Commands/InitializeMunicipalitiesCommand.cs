// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;

namespace server.src.Application.Geography.Administrative.Municipalities.Commands;

public record InitializeMunicipalitiesCommand(List<CreateMunicipalityDto> Dto) : IRequest<Response<string>>;

public class InitializeMunicipalitiesHandler : IRequestHandler<InitializeMunicipalitiesCommand, Response<string>>
{
    private readonly IMunicipalityCommands _municipalityCommands;

    public InitializeMunicipalitiesHandler(IMunicipalityCommands municipalityCommands)
    {
        _municipalityCommands = municipalityCommands;
    }

    public async Task<Response<string>> Handle(InitializeMunicipalitiesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _municipalityCommands.CreateMunicipalityAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing municipalities.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize municipalities.");

        return new Response<string>()
            .WithMessage("Success initializing municipalities.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
