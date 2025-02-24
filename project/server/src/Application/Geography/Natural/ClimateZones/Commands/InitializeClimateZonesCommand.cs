// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.ClimateZones.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Application.Geography.Natural.ClimateZones.Commands;

public record InitializeClimateZonesCommand(List<CreateClimateZoneDto> Dto) : IRequest<Response<string>>;

public class InitializeClimateZonesHandler : IRequestHandler<InitializeClimateZonesCommand, Response<string>>
{
    private readonly IClimateZoneCommands _climatezoneCommands;

    public InitializeClimateZonesHandler(IClimateZoneCommands climatezoneCommands)
    {
        _climatezoneCommands = climatezoneCommands;
    }

    public async Task<Response<string>> Handle(InitializeClimateZonesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _climatezoneCommands.CreateClimateZoneAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing climate zones.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize climate zones.");

        return new Response<string>()
            .WithMessage("Success initializing climate zones.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
