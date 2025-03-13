// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Application.Geography.Administrative.States.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Continents.Resources;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Countries.Resources;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedStatesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedStatesHandler : IRequestHandler<SeedStatesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IStateCommands _stateCommands;

    public SeedStatesHandler( ICommonRepository commonRepository, 
        IStateCommands stateCommands)
    {
        _commonRepository = commonRepository;
        _stateCommands = stateCommands;
    }

    public async Task<Response<string>> Handle(SeedStatesCommand command, CancellationToken token = default)
    {
        if(command.BasePath.Equals(string.Empty))
            return new Response<string>()
                .WithMessage("Base path is empty")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
                
        var projection = (Expression<Func<Country, CountryLookup>>)(c => 
            new CountryLookup 
            { 
                Id = c.Id,
                Code = c.Code,
                ContinentId = c.ContinentId
            });

        var countries = await _commonRepository.GetResultPickerAsync(
            projection: projection,
            token: token
        );

        var statesDto = new List<CreateStateDto>();
        foreach (var country in countries)
        {
            var states = await ProcessStatesForCountry(
                country,
                command.BasePath, 
                token
            );
            if (states == null || states.Count == 0)
                continue;
            
            statesDto.AddRange(states);
        }

        if (statesDto == null || statesDto.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No states found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var statesResponse = await _stateCommands.InitializeStatesAsync(statesDto, token);
        if (!statesResponse.Success)
            return new Response<string>()
                .WithMessage(statesResponse.Message!)
                .WithSuccess(statesResponse.Success)
                .WithFailures(statesResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in states seeding")
            .WithSuccess(statesResponse.Success)
            .WithFailures(statesResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("States seeding was successful!");
    }

    private async Task<List<CreateStateDto>> ProcessStatesForCountry(
        CountryLookup countryLookup,
        string basePath,
        CancellationToken token = default)
    {
        var filterContinent = new Expression<Func<Continent, bool>>[] {
            c => c.Id == countryLookup.ContinentId
        };
        var projection = (Expression<Func<Continent, ContinentLookup>>)(c => 
            new ContinentLookup 
            { 
                Id = c.Id,
                Code = c.Code
            });
        var continent = await _commonRepository.GetResultByIdAsync(
            filterContinent, 
            projection: projection, 
            token: token
        );

        if (continent is null)
            return [];

        var jsonFilePath =  @$"{basePath}\
                {continent.Code}\
                {countryLookup.Code}.json"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
        if (!File.Exists(jsonFilePath))
            return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var states = JsonSerializer.Deserialize<List<ImportStateDto>>(json);

        return states!.Select(state => 
            state.ImportStateMapping(countryLookup.Id)).ToList();
    }
}