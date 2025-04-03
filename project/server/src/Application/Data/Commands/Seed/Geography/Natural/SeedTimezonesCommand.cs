// packages
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Timezones.Interfaces;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Data.Commands.Seed.Geography.Natural;

public record SeedTimezonesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedTimezonesHandler : IRequestHandler<SeedTimezonesCommand, Response<string>>
{
    private readonly ITimezoneCommands _timezoneCommands;

    public SeedTimezonesHandler(ITimezoneCommands timezoneCommands)
    {
        _timezoneCommands = timezoneCommands;
    }

    public async Task<Response<string>> Handle(SeedTimezonesCommand command, CancellationToken token = default)
    {
        var jsonFilePath = @$"{command.BasePath}\Timezones.json";
        if (!File.Exists(jsonFilePath))
            return new Response<string>()
                .WithMessage("JSON file not found.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
    
        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var timezones = JsonSerializer.Deserialize<List<CreateTimezoneDto>>(json);

        if (timezones == null || timezones.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No timezones found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var timezonesResponse = await _timezoneCommands.InitializeTimezonesAsync(timezones, token);
        if (!timezonesResponse.Success)
            return new Response<string>()
                .WithMessage(timezonesResponse.Message!)
                .WithSuccess(timezonesResponse.Success)
                .WithFailures(timezonesResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in timezones seeding")
            .WithSuccess(timezonesResponse.Success)
            .WithFailures(timezonesResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Timezones seeding was successful!");
    }
}
