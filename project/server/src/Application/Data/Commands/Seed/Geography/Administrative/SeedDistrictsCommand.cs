// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Continents.Resources;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Countries.Resources;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Resources;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.Regions.Resources;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Domain.Geography.Administrative.States.Resources;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedDistrictsCommand(string BasePath) : IRequest<Response<string>>;

public class SeedDistrictsHandler : IRequestHandler<SeedDistrictsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IDistrictCommands _districtCommands;

    public SeedDistrictsHandler(ICommonRepository commonRepository, IDistrictCommands districtCommands)
    {   
        _commonRepository = commonRepository;
        _districtCommands = districtCommands;
    }

    public async Task<Response<string>> Handle(SeedDistrictsCommand command, CancellationToken token = default)
    {
        if(command.BasePath.Equals(string.Empty))
            return new Response<string>()
                .WithMessage("Base path is empty")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");

        var projection = (Expression<Func<Municipality, MunicipalityLookup>>)(m => 
            new MunicipalityLookup 
            { 
                Id = m.Id,
                Code = m.Code,
                RegionId = m.RegionId,
            });

        var municipalities = await _commonRepository.GetResultPickerAsync(
            projection: projection,
            token: token
        );

        var districtsDto = new List<CreateDistrictDto>();
        foreach (var municipality in municipalities)
        {
            var districts = await ProcessDistrictsForMunicipality(
                municipality,
                command.BasePath,
                token
            );
            if (districts == null || districts.Count == 0)
                continue;
            districtsDto.AddRange(districts);
        }

        if (districtsDto == null || districtsDto.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No districts found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var districtsResponse = await _districtCommands.InitializeDistrictsAsync(districtsDto, token);
        if (!districtsResponse.Success)
            return new Response<string>()
                .WithMessage(districtsResponse.Message!)
                .WithSuccess(districtsResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithFailures(districtsResponse.Failures)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in municipalities seeding")
            .WithSuccess(districtsResponse.Success)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(districtsResponse.Failures)
            .WithData("Municipalities seeding was successful!");
    }

    private async Task<List<CreateDistrictDto>> ProcessDistrictsForMunicipality(
        MunicipalityLookup municipalityLookup,
        string basePath,
        CancellationToken token = default)
    {
        var filterRegion = new Expression<Func<Region, bool>>[] {
            r => r.Id == municipalityLookup.RegionId
        };
        var projectionRegion = (Expression<Func<Region, RegionLookup>>)(r => 
            new RegionLookup 
            { 
                Id = r.Id,
                Code = r.Code,
                StateId = r.StateId
            });
        var region = await _commonRepository.GetResultByIdAsync(
            filterRegion, 
            projection: projectionRegion, 
            token: token
        );

        if (region is null) return [];

        var filterState = new Expression<Func<State, bool>>[] {
            s => s.Id == region.StateId
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
                {region.Code}\
                {municipalityLookup.Code}.json"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
        if (!File.Exists(jsonFilePath)) return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var districts = JsonSerializer.Deserialize<List<ImportDistrictDto>>(json);

        return districts!.Select(district => 
            district.ImportDistrictMapping(municipalityLookup.Id)).ToList();
    }
}