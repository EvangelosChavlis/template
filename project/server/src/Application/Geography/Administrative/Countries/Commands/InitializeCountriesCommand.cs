// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Dtos;

namespace server.src.Application.Geography.Administrative.Countries.Commands;

public record InitializeCountriesCommand(List<CreateCountryDto> Dto) : IRequest<Response<string>>;

public class InitializeCountriesHandler : IRequestHandler<InitializeCountriesCommand, Response<string>>
{
    private readonly ICountryCommands _countryCommands;

    public InitializeCountriesHandler(ICountryCommands countryCommands)
    {
        _countryCommands = countryCommands;
    }

    public async Task<Response<string>> Handle(InitializeCountriesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _countryCommands.CreateCountryAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing countries.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize country.");

        return new Response<string>()
            .WithMessage("Success initializing countries.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
