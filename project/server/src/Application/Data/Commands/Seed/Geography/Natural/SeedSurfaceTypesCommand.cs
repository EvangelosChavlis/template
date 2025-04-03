// packages
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

namespace server.src.Application.Data.Commands.Seed.Geography.Natural;

public record SeedSurfaceTypesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedSurfaceTypesHandler : IRequestHandler<SeedSurfaceTypesCommand, Response<string>>
{
    private readonly ISurfaceTypeCommands _surfaceTypeCommands;

    public SeedSurfaceTypesHandler(ISurfaceTypeCommands surfaceTypeCommands)
    {
        _surfaceTypeCommands = surfaceTypeCommands;
    }

    public async Task<Response<string>> Handle(SeedSurfaceTypesCommand command, CancellationToken token = default)
    {
        var jsonFilePath = @$"{command.BasePath}\SurfaceTypes.json";
        if (!File.Exists(jsonFilePath))
            return new Response<string>()
                .WithMessage("JSON file not found.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
    
        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var surfacetypes = JsonSerializer.Deserialize<List<CreateSurfaceTypeDto>>(json);

        if (surfacetypes == null || surfacetypes.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No surfacetypes found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var surfacetypesResponse = await _surfaceTypeCommands.InitializeSurfaceTypesAsync(surfacetypes, token);
        if (!surfacetypesResponse.Success)
            return new Response<string>()
                .WithMessage(surfacetypesResponse.Message!)
                .WithSuccess(surfacetypesResponse.Success)
                .WithFailures(surfacetypesResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in surface types seeding")
            .WithSuccess(surfacetypesResponse.Success)
            .WithFailures(surfacetypesResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Surface types seeding was successful!");
    }
}
