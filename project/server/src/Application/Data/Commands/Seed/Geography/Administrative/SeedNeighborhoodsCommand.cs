// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Districts.Resources;
using server.src.Domain.Geography.Administrative.Municipalities.Resources;
using server.src.Domain.Geography.Administrative.Regions.Resources;
using server.src.Domain.Geography.Administrative.States.Resources;
using server.src.Domain.Geography.Administrative.Countries.Resources;
using server.src.Domain.Geography.Administrative.Continents.Resources;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedNeighborhoodsCommand(string BasePath) : IRequest<Response<string>>;

public class SeedNeighborhoodsHandler : IRequestHandler<SeedNeighborhoodsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly INeighborhoodCommands _neighborhoodCommands;

    public SeedNeighborhoodsHandler(ICommonRepository commonRepository, INeighborhoodCommands neighborhoodCommands)
    {   
        _commonRepository = commonRepository;
        _neighborhoodCommands = neighborhoodCommands;
    }

    public async Task<Response<string>> Handle(SeedNeighborhoodsCommand command, CancellationToken token = default)
    {
        if(command.BasePath.Equals(string.Empty))
            return new Response<string>()
                .WithMessage("Base path is empty")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");

        var projection = (Expression<Func<District, DistrictLookup>>)(d => 
            new DistrictLookup 
            { 
                Id = d.Id,
                Code = d.Code,
                MunicipalityId = d.MunicipalityId,
            });

        var districts = await _commonRepository.GetResultPickerAsync(
            projection: projection,
            token: token
        );

        var neighborhoodsDto = new List<CreateNeighborhoodDto>();
        foreach (var distict in districts)
        {
            var neighborhoods = await ProcessNeighborhoodsForDistrict(
                distict,
                command.BasePath,
                token
            );
            if (neighborhoods == null || neighborhoods.Count == 0)
                continue;
            
            neighborhoodsDto.AddRange(neighborhoods);
        }

        if (neighborhoodsDto == null || neighborhoodsDto.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No neighborhoods found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var neighborhoodsResponse = await _neighborhoodCommands.InitializeNeighborhoodsAsync(neighborhoodsDto, token);
        if (!neighborhoodsResponse.Success)
            return new Response<string>()
                .WithMessage(neighborhoodsResponse.Message!)
                .WithSuccess(neighborhoodsResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithFailures(neighborhoodsResponse.Failures)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in municipalities seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(neighborhoodsResponse.Failures)
            .WithData("Municipalities seeding was successful!");
    }

    private async Task<List<CreateNeighborhoodDto>> ProcessNeighborhoodsForDistrict(
        DistrictLookup districtLookup,
        string basePath,
        CancellationToken token = default
    )
    {
        var filterMunicipality = new Expression<Func<Municipality, bool>>[] {
            m => m.Id == districtLookup.MunicipalityId
        };
        var projectionMunicipality = (Expression<Func<Municipality, MunicipalityLookup>>)(m => 
            new MunicipalityLookup 
            { 
                Id = m.Id,
                Code = m.Code,
                RegionId = m.RegionId
            });
        var municipality = await _commonRepository.GetResultByIdAsync(
            filterMunicipality, 
            projection: projectionMunicipality, 
            token: token
        );

        if (municipality is null) return [];

        var filterRegion = new Expression<Func<Region, bool>>[] {
            r => r.Id == municipality.RegionId
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
            
        var jsonFilePath = @$"{basePath}\
                {continent.Code}\
                {country.Code}\
                {state.Code}\
                {region.Code}\
                {municipality.Code}\
                {districtLookup.Code}.json"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
        if (!File.Exists(jsonFilePath)) return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var neighborhoods = JsonSerializer.Deserialize<List<ImportNeighborhoodDto>>(json);

        return neighborhoods!.Select(neighborhood => 
            neighborhood.ImportNeighborhoodMapping(districtLookup.Id)).ToList();
    }
}