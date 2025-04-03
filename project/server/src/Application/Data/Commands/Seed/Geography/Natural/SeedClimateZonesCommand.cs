// packages
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.ClimateZones.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Application.Data.Commands.Seed.Geography.Natural;

public record SeedClimateZonesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedClimateZonesHandler : IRequestHandler<SeedClimateZonesCommand, Response<string>>
{
    private readonly IClimateZoneCommands _climatezonesCommands;

    public SeedClimateZonesHandler(IClimateZoneCommands climatezonesCommands)
    {
        _climatezonesCommands = climatezonesCommands;
    }

    public async Task<Response<string>> Handle(SeedClimateZonesCommand command, CancellationToken token = default)
    {
        var jsonFilePath = @$"{command.BasePath}\ClimateZones.json";
        if (!File.Exists(jsonFilePath))
            return new Response<string>()
                .WithMessage("JSON file not found.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
    
        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var climatezones = JsonSerializer.Deserialize<List<CreateClimateZoneDto>>(json);

        if (climatezones == null || climatezones.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No climatezoness found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var climatezonessResponse = await _climatezonesCommands.InitializeClimateZonesAsync(climatezones, token);
        if (!climatezonessResponse.Success)
            return new Response<string>()
                .WithMessage(climatezonessResponse.Message!)
                .WithSuccess(climatezonessResponse.Success)
                .WithFailures(climatezonessResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in climatezoness seeding")
            .WithSuccess(climatezonessResponse.Success)
            .WithFailures(climatezonessResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("ClimateZones seeding was successful!");
    }
}
