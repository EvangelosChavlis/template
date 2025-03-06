// packages
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedContinentsCommand() : IRequest<Response<string>>;

public class SeedContinentsHandler : IRequestHandler<SeedContinentsCommand, Response<string>>
{
    private readonly string _basePath = @"..\..\..\server\assets\";
    private readonly IContinentCommands _continentCommands;

    public SeedContinentsHandler(IContinentCommands continentCommands)
    {
        _continentCommands = continentCommands;
    }

    public async Task<Response<string>> Handle(SeedContinentsCommand command, CancellationToken token = default)
    {
        var jsonFilePath = Path.Combine(_basePath, "Continents.json");
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
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in continents seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Continents seeding was successful!");
    }
}