// source
using server.src.Application.Data.Commands;
using server.src.Application.Data.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Application.Common.Services;
using server.src.Application.Data.Commands.Seed.Geography.Administrative;

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

    public async Task<Response<string>> SeedContinentsAsync(CancellationToken token = default)
    {
        var command = new SeedContinentsCommand();
        return await _requestExecutor
            .Execute<SeedContinentsCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedCountriesAsync(CancellationToken token = default)
    {
        var command = new SeedCountriesCommand();
        return await _requestExecutor
            .Execute<SeedCountriesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> SeedStatesAsync(CancellationToken token = default)
    {
        var command = new SeedStatesCommand();
        return await _requestExecutor
            .Execute<SeedStatesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ClearDataAsync(CancellationToken token = default)
    {
        var command = new ClearDataCommand();
        return await _requestExecutor
            .Execute<ClearDataCommand, Response<string>>(command, token);
    }
}