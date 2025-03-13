// packages
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Regions.Interfaces;
using server.src.Application.Geography.Administrative.States.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Continents.Resources;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Countries.Resources;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Domain.Geography.Administrative.States.Resources;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Data.Commands.Seed.Geography.Administrative;

public record SeedRegionsCommand(string BasePath) : IRequest<Response<string>>;

public class SeedRegionsHandler : IRequestHandler<SeedRegionsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IRegionCommands _regionCommands;

    public SeedRegionsHandler(ICommonRepository commonRepository, IRegionCommands regionCommands)
    {
        _commonRepository = commonRepository;
        _regionCommands = regionCommands;
    }

    public async Task<Response<string>> Handle(SeedRegionsCommand command, CancellationToken token = default)
    {
        if(command.BasePath.Equals(string.Empty))
            return new Response<string>()
                .WithMessage("Base path is empty")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");     

        var projection = (Expression<Func<State, StateLookup>>)(c => 
            new StateLookup 
            { 
                Id = c.Id,
                Code = c.Code,
                CountryId = c.CountryId,
            });

        var states = await _commonRepository.GetResultPickerAsync(
            projection: projection,
            token: token
        );

        var regionsDto = new List<CreateRegionDto>();
        foreach (var state in states)
        {
            var regions = await ProcessRegionsForState(
                state, 
                command.BasePath,
                token
            );
            if (regions == null || regions.Count == 0)
                continue;
            
            regionsDto.AddRange(regions);
        }

        if (regionsDto == null || regionsDto.Count == 0)
        {
            return new Response<string>()
                .WithMessage("No region found in the JSON file.")
                .WithSuccess(false)
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithData("Data seeding failed!");
        }

        var regionsResponse = await _regionCommands.InitializeRegionsAsync(regionsDto, token);
        if (!regionsResponse.Success)
            return new Response<string>()
                .WithMessage(regionsResponse.Message!)
                .WithSuccess(regionsResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithFailures(regionsResponse.Failures)
                .WithData("Data seeding failed!");

        return new Response<string>()
            .WithMessage("Success in regions seeding")
            .WithSuccess(regionsResponse.Success)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(regionsResponse.Failures)
            .WithData("States seeding was successful!");
    }

    private async Task<List<CreateRegionDto>> ProcessRegionsForState(
        StateLookup stateLookup,
        string basePath, 
        CancellationToken token = default)
    {
        var filterCountry = new Expression<Func<Country, bool>>[] {
            c => c.Id == stateLookup.CountryId
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
                {stateLookup.Code}.json"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim();
        if (!File.Exists(jsonFilePath)) return [];

        var json = await File.ReadAllTextAsync(jsonFilePath, token);
        var states = JsonSerializer.Deserialize<List<ImportRegionDto>>(json);

        return states!.Select(region => 
            region.ImportRegionMapping(stateLookup.Id)).ToList();
    }
}