// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Stations.Interfaces;
using server.src.Application.Geography.Administrative.Stations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
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
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedStationsCommand(string BasePath) : IRequest<Response<string>>;

public class SeedStationsHandler : IRequestHandler<SeedStationsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IStationCommands _stationCommands;

    public SeedStationsHandler(ICommonRepository commonRepository, IStationCommands stationCommands)
    {
        _commonRepository = commonRepository;
        _stationCommands = stationCommands;
    }

    public async Task<Response<string>> Handle(SeedStationsCommand command, CancellationToken token = default)
    {
        if (command.BasePath.Equals(string.Empty))
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

        var stationsDto = new List<CreateStationDto>();
        foreach (var distict in districts)
        {
            var stations = await ProcessStationsForDistrict(
                distict,
                command.BasePath,
                token
            );
            if (stations == null || stations.Count == 0)
                continue;

            stationsDto.AddRange(stations);
        }

        if (stationsDto == null || stationsDto.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No stations found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var stationsResponse = await _stationCommands.InitializeStationsAsync(stationsDto, token);
        if (!stationsResponse.Success)
            return new Response<string>()
                .WithMessage(stationsResponse.Message!)
                .WithSuccess(stationsResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithFailures(stationsResponse.Failures)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in municipalities seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(stationsResponse.Failures)
            .WithData("Municipalities seeding was successful!");
    }

    private async Task<List<CreateStationDto>> ProcessStationsForDistrict(
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
        var stations = JsonSerializer.Deserialize<List<ImportStationDto>>(json);

        if (stations == null || stations.Count == 0) return [];

        var dtos = new List<CreateStationDto>();
        foreach (var station in stations)
        {
            var filterNeighborhood = new Expression<Func<Neighborhood, bool>>[] {
                n => n.Code == station.Code
            };
            var projectionNeighborhood = (Expression<Func<Neighborhood, Guid>>)(n => n.Id);
            var neighborhoodId = await _commonRepository.GetResultByIdAsync(
                filters: filterNeighborhood,
                projection: projectionNeighborhood,
                token: token
            );

            if (neighborhoodId == Guid.Empty) continue;

            var item = station.ImportStationMapping(districtLookup.Id);
            dtos.Add(item);
        }

        return dtos;
    }
}