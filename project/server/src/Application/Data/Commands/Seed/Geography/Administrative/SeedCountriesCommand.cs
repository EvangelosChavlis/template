// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Continents.Resources;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedCountriesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedCountriesHandler : IRequestHandler<SeedCountriesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICountryCommands _countryCommands;

    public SeedCountriesHandler(ICommonRepository commonRepository, 
        ICountryCommands countryCommands)
    {
        _commonRepository = commonRepository;
        _countryCommands = countryCommands;
    }

    public async Task<Response<string>> Handle(SeedCountriesCommand command, CancellationToken token = default)
    {
        if(command.BasePath.Equals(string.Empty))
            return new Response<string>()
                .WithMessage("Base path is empty")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
                
        var projection = (Expression<Func<Continent, ContinentLookup>>)(c => 
            new ContinentLookup 
            { 
                Id = c.Id,
                Code = c.Code, 
            });
        var continents = await _commonRepository.GetResultPickerAsync(
            projection: projection, 
            token: token
        );

        var countriesDto = new List<CreateCountryDto>();
        foreach (var continent in continents)
        {
            var countries = await ProcessCountriesForContinent(
                continent,
                command.BasePath, 
                token
            );
            if (countries == null || countries.Count == 0)
                continue;
            countriesDto.AddRange(countries);
        }

        if (countriesDto == null || countriesDto.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No countries found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var countriesResponse = await _countryCommands.InitializeCountriesAsync(countriesDto, token);
        if (!countriesResponse.Success)
            return new Response<string>()
                .WithMessage(countriesResponse.Message!)
                .WithSuccess(countriesResponse.Success)
                .WithFailures(countriesResponse.Failures)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in countries seeding")
            .WithSuccess(countriesResponse.Success)
            .WithFailures(countriesResponse.Failures)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Countries seeding was successful!");
    }

    private async Task<List<CreateCountryDto>> ProcessCountriesForContinent(
        ContinentLookup continentLookup,
        string basePath,
        CancellationToken token = default)
    {
        var jsonFilePath =  @$"{basePath}\
                {continentLookup.Code}.json"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
        if (!File.Exists(jsonFilePath))
            return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var countries = JsonSerializer.Deserialize<List<ImportCountryDto>>(json);
        
        return countries!.Select(country => 
            country.ImportCountryMapping(continentLookup.Id)).ToList();
    }
}