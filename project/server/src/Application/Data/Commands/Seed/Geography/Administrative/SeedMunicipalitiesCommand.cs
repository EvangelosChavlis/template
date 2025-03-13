// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Continents.Resources;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Countries.Resources;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.Regions.Resources;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Domain.Geography.Administrative.States.Resources;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedMunicipalitiesCommand(string BasePath) : IRequest<Response<string>>;

public class SeedMunicipalitiesHandler : IRequestHandler<SeedMunicipalitiesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IMunicipalityCommands _municipalityCommands;

    public SeedMunicipalitiesHandler(ICommonRepository commonRepository, IMunicipalityCommands municipalityCommands)
    {   
        _commonRepository = commonRepository;
        _municipalityCommands = municipalityCommands;
    }

    public async Task<Response<string>> Handle(SeedMunicipalitiesCommand command, CancellationToken token = default)
    {
        if(command.BasePath.Equals(string.Empty))
            return new Response<string>()
                .WithMessage("Base path is empty")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");

        var projection = (Expression<Func<Region, RegionLookup>>)(c => 
            new RegionLookup 
            { 
                Id = c.Id,
                Code = c.Code,
                StateId = c.StateId,
            });

        var regions = await _commonRepository.GetResultPickerAsync(
            projection: projection,
            token: token
        );

        var municipalitiesDto = new List<CreateMunicipalityDto>();
        foreach (var region in regions)
        {
            var municipalities = await ProcessMunicipalitiesForRegion(
                region,
                command.BasePath, 
                token
            );
            if (municipalities == null || municipalities.Count == 0)
                continue;
            
            municipalitiesDto.AddRange(municipalities);
        }

        var municipalitiesResponse = await _municipalityCommands.InitializeMunicipalitiesAsync(municipalitiesDto, token);
        if (!municipalitiesResponse.Success)
            return new Response<string>()
                .WithMessage(municipalitiesResponse.Message!)
                .WithSuccess(municipalitiesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithFailures(municipalitiesResponse.Failures)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in municipalities seeding")
            .WithSuccess(municipalitiesResponse.Success)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(municipalitiesResponse.Failures)
            .WithData("Municipalities seeding was successful!");
    }

    private async Task<List<CreateMunicipalityDto>> ProcessMunicipalitiesForRegion(
        RegionLookup regionLookup,
        string basePath,
        CancellationToken token = default)
    {
        var filterState = new Expression<Func<State, bool>>[] {
            s => s.Id == regionLookup.StateId
        };
        var projectionState = (Expression<Func<State, StateLookup>>)(s => 
            new StateLookup 
            { 
                Id = s.Id,
                Code = s.Code,
                CountryId = s.CountryId
            });
        var state = await _commonRepository.GetResultByIdAsync(
            filterState, 
            projection: projectionState, 
            token: token
        );

        if (state is null) return [];

        var filterCountry = new Expression<Func<Country, bool>>[] {
            c => c.Id == state.CountryId
        };
        var projectionCountry = (Expression<Func<Country, CountryLookup>>)(c => 
            new CountryLookup 
            { 
                Id = c.Id,
                Code = c.Code,
                ContinentId = c.ContinentId
            });
        var country = await _commonRepository.GetResultByIdAsync(
            filterCountry, 
            projection: projectionCountry, 
            token: token
        );

        if (country is null) return [];

        var filterContinent = new Expression<Func<Continent, bool>>[] {
            c => c.Id == country.ContinentId
        };
        var projectionContinent = (Expression<Func<Continent, ContinentLookup>>)(c => 
            new ContinentLookup 
            { 
                Id = c.Id,
                Code = c.Code
            });
        var continent = await _commonRepository.GetResultByIdAsync(
            filterContinent, 
            projection: projectionContinent, 
            token: token
        );

        if (continent is null) return [];
            
        var jsonFilePath =  @$"{basePath}\
                {continent.Code}\
                {country.Code}\
                {state.Code}\
                {regionLookup.Code}.json"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
        if (!File.Exists(jsonFilePath)) return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var municipalities = JsonSerializer.Deserialize<List<ImportMunicipalityDto>>(json);

        return municipalities!.Select(municipality => 
            municipality.ImportMunicipalityMapping(regionLookup.Id)).ToList();
    }
}