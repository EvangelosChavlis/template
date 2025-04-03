// packages
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

namespace server.src.Application.Data.Commands.Seed.Geography.Natural;

public record SeedNaturalFeaturesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedNaturalFeaturesHandler : IRequestHandler<SeedNaturalFeaturesCommand, Response<string>>
{
    private readonly INaturalFeatureCommands _naturalFeatureCommands;

    public SeedNaturalFeaturesHandler(INaturalFeatureCommands naturalFeatureCommands)
    {
        _naturalFeatureCommands = naturalFeatureCommands;
    }

    public async Task<Response<string>> Handle(SeedNaturalFeaturesCommand command, CancellationToken token = default)
    {
        var jsonFilePath = @$"{command.BasePath}\NaturalFeatures.json";
        if (!File.Exists(jsonFilePath))
            return new Response<string>()
                .WithMessage("JSON file not found.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
    
        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var naturalfeatures = JsonSerializer.Deserialize<List<CreateNaturalFeatureDto>>(json);

        if (naturalfeatures == null || naturalfeatures.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No naturalfeatures found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var naturalfeaturesResponse = await _naturalFeatureCommands.InitializeNaturalFeaturesAsync(naturalfeatures, token);
        if (!naturalfeaturesResponse.Success)
            return new Response<string>()
                .WithMessage(naturalfeaturesResponse.Message!)
                .WithSuccess(naturalfeaturesResponse.Success)
                .WithFailures(naturalfeaturesResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in surface types seeding")
            .WithSuccess(naturalfeaturesResponse.Success)
            .WithFailures(naturalfeaturesResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Surface types seeding was successful!");
    }
}
