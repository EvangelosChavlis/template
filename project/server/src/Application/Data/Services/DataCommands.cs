// source
using server.src.Application.Data.Commands;
using server.src.Application.Data.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Application.Common.Services;
using server.src.Application.Data.Commands.Seed.Geography.Administrative;
using server.src.Application.Data.Commands.Seed.Geography.Natural;

namespace server.src.Application.Auth.Roles.Services;

public class DataCommands : IDataCommands
{
    private readonly RequestExecutor _requestExecutor;

    public DataCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> SeedDataAsync(CancellationToken token = default)
    {
        var command = new SeedDataCommand();
        return await _requestExecutor
            .Execute<SeedDataCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedTimezonesAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedTimezonesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedTimezonesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedClimateZonesAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedClimateZonesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedClimateZonesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedSurfaceTypeAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedSurfaceTypesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedSurfaceTypesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedNaturalFeatureAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedNaturalFeaturesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedNaturalFeaturesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedLocationsAsync(CancellationToken token = default)
    {
        var command = new SeedLocationsCommand();
        return await _requestExecutor
            .Execute<SeedLocationsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedContinentsAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedContinentsCommand(basePath);
        return await _requestExecutor
            .Execute<SeedContinentsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedCountriesAsync(string basePath,
        CancellationToken token = default)
    {
        var command = new SeedCountriesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedCountriesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedStatesAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedStatesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedStatesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedRegionsAsync(string basePath, 
        CancellationToken token = default)
    {
        var command = new SeedRegionsCommand(basePath);
        return await _requestExecutor
            .Execute<SeedRegionsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedMunicipalitiesAsync(string basePath,
        CancellationToken token = default)
    {
        var command = new SeedMunicipalitiesCommand(basePath);
        return await _requestExecutor
            .Execute<SeedMunicipalitiesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedDistrictsAsync(string basePath,
        CancellationToken token = default)
    {
        var command = new SeedDistrictsCommand(basePath);
        return await _requestExecutor
            .Execute<SeedDistrictsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedNeighborhoodAsync(string basePath,
        CancellationToken token = default)
    {
        var command = new SeedNeighborhoodsCommand(basePath);
        return await _requestExecutor
            .Execute<SeedNeighborhoodsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ClearDataAsync(CancellationToken token = default)
    {
        var command = new ClearDataCommand();
        return await _requestExecutor
            .Execute<ClearDataCommand, Response<string>>(command, token);
    }
}