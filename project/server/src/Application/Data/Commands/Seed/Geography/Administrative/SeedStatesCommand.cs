// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Application.Geography.Administrative.States.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedStatesCommand() : IRequest<Response<string>>;

public class SeedStatesHandler : IRequestHandler<SeedStatesCommand, Response<string>>
{
    private readonly string _basePath = @"..\..\..\server\assets\countries\";
    private readonly ICommonRepository _commonRepository;
    private readonly IStateCommands _stateCommands;

    public SeedStatesHandler(ICommonRepository commonRepository, IStateCommands stateCommands)
    {
        _commonRepository = commonRepository;
        _stateCommands = stateCommands;
    }

    public async Task<Response<string>> Handle(SeedStatesCommand command, CancellationToken token = default)
    {
        var dtoStates = new List<CreateStateDto>();

        var projection = (Expression<Func<Country, Country>>)(c => 
            new Country 
            { 
                Id = c.Id,
                Code = c.Code
            });

        var countries = await _commonRepository.GetResultPickerAsync(
            projection: projection,
            token: token
        );

        foreach (var country in countries)
        {
            var states = await ProcessStatesForCountry(country.Id, country.Code, token);
            if (states == null || states.Count == 0)
                continue;
            dtoStates.AddRange(states);
        }

        var statesResponse = await _stateCommands.InitializeStatesAsync(dtoStates, token);

        if (!statesResponse.Success)
            return new Response<string>()
                .WithMessage(statesResponse.Message!)
                .WithSuccess(statesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in states seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("States seeding was successful!");
    }

    private async Task<List<CreateStateDto>> ProcessStatesForCountry(Guid countryId, 
        string jsonFileName, CancellationToken token)
    {
        var jsonFilePath = _basePath + $"{jsonFileName}.json";
        if (!File.Exists(jsonFilePath))
            return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var states = JsonSerializer.Deserialize<List<ImportStateDto>>(json);

        return states!.Select(state => state.ImportStateMapping(countryId)).ToList();
    }
}