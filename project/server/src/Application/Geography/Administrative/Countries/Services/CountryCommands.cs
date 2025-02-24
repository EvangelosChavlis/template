// source
using server.src.Application.Geography.Administrative.Countries.Commands;
using server.src.Application.Geography.Administrative.Countries.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Countries.Services;

public class CountryCommands : ICountryCommands
{
    private readonly RequestExecutor _requestExecutor;

    public CountryCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeCountriesAsync(List<CreateCountryDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeCountriesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeCountriesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateCountryAsync(CreateCountryDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateCountryCommand(dto);
        return await _requestExecutor
            .Execute<CreateCountryCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateCountryAsync(Guid id, 
        UpdateCountryDto dto, CancellationToken token = default)
    {
        var command = new UpdateCountryCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateCountryCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateCountryAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateCountryCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateCountryCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateCountryAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateCountryCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateCountryCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteCountryAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteCountryCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteCountryCommand, Response<string>>(command, token);
    }
}