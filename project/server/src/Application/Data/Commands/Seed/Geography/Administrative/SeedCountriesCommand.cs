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
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedCountriesCommand() : IRequest<Response<string>>;

public class SeedCountriesHandler : IRequestHandler<SeedCountriesCommand, Response<string>>
{
    private readonly string _basePath = @"..\..\..\server\assets\continents\";
    private readonly ICommonRepository _commonRepository;
    private readonly ICountryCommands _countryCommands;

    public SeedCountriesHandler(ICommonRepository commonRepository, ICountryCommands countryCommands)
    {
        _commonRepository = commonRepository;
        _countryCommands = countryCommands;
    }

    public async Task<Response<string>> Handle(SeedCountriesCommand request, CancellationToken token = default)
    {
        var dtoCountries = new List<CreateCountryDto>();

        var projection = (Expression<Func<Continent, Continent>>)(c => 
            new Continent 
            { 
                Id = c.Id,
                Code = c.Code, 
            });
        var continents = await _commonRepository.GetResultPickerAsync(
            projection: projection, 
            token: token
        );


        foreach (var continent in continents)
        {
            var countries = await ProcessCountriesForContinent(continent.Id, continent.Code, token);
            if (countries == null || countries.Count == 0)
                continue;
            dtoCountries.AddRange(countries);
        }

        var countriesResponse = await _countryCommands.InitializeCountriesAsync(dtoCountries, token);

        if (!countriesResponse.Success)
            return new Response<string>()
                .WithMessage(countriesResponse.Message!)
                .WithSuccess(countriesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in countries seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Countries seeding was successful!");
    }

    private async Task<List<CreateCountryDto>> ProcessCountriesForContinent(Guid id, 
        string jsonFileName, CancellationToken token = default)
    {
        var jsonFilePath = _basePath + $"{jsonFileName}.json";
        if (!File.Exists(jsonFilePath))
            return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var countries = JsonSerializer.Deserialize<List<ImportCountryDto>>(json);
        
        return countries!.Select(country => country.ImportCountryMapping(id)).ToList();
    }
}