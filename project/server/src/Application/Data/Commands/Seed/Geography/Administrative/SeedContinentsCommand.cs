// packages
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedContinentsCommand(string BasePath) : IRequest<Response<string>>;

public class SeedContinentsHandler : IRequestHandler<SeedContinentsCommand, Response<string>>
{
    private readonly IContinentCommands _continentCommands;

    public SeedContinentsHandler(IContinentCommands continentCommands)
    {
        _continentCommands = continentCommands;
    }

    public async Task<Response<string>> Handle(SeedContinentsCommand command, CancellationToken token = default)
    {
        var jsonFilePath = @$"{command.BasePath}\Continents.json";
        if (!File.Exists(jsonFilePath))
            return new Response<string>()
                .WithMessage("JSON file not found.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
    
        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var continents = JsonSerializer.Deserialize<List<CreateContinentDto>>(json);

        if (continents == null || continents.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No continents found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var continentsResponse = await _continentCommands.InitializeContinentsAsync(continents, token);
        if (!continentsResponse.Success)
            return new Response<string>()
                .WithMessage(continentsResponse.Message!)
                .WithSuccess(continentsResponse.Success)
                .WithFailures(continentsResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in continents seeding")
            .WithSuccess(continentsResponse.Success)
            .WithFailures(continentsResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Continents seeding was successful!");
    }
}