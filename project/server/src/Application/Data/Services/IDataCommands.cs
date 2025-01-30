// source
using server.src.Application.Data.Commands;
using server.src.Application.Data.Interfaces;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Roles.Services;

public class DataCommands : IDataCommands
{
    private readonly IRequestHandler<SeedDataCommand, Response<string>> _seedDataHander;
    private readonly IRequestHandler<ClearDataCommand, Response<string>> _clearDataHandler;

    public DataCommands(
        IRequestHandler<SeedDataCommand, Response<string>> seedDataHander,
        IRequestHandler<ClearDataCommand, Response<string>> clearDataHandler)
    {
        _seedDataHander = seedDataHander;
        _clearDataHandler = clearDataHandler;
    }

    public async Task<Response<string>> SeedDataAsync(CancellationToken token = default)
    {
        var command = new SeedDataCommand();
        return await _seedDataHander.Handle(command, token);
    }

    public async Task<Response<string>> ClearDataAsync(CancellationToken token = default)
    {
        var command = new ClearDataCommand();
        return await _clearDataHandler.Handle(command, token);
    }
}