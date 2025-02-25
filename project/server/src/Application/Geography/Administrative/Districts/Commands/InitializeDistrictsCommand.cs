// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Application.Geography.Administrative.Districts.Commands;

public record InitializeDistrictsCommand(List<CreateDistrictDto> Dto) : IRequest<Response<string>>;

public class InitializeDistrictsHandler : IRequestHandler<InitializeDistrictsCommand, Response<string>>
{
    private readonly IDistrictCommands _districtCommands;

    public InitializeDistrictsHandler(IDistrictCommands districtCommands)
    {
        _districtCommands = districtCommands;
    }

    public async Task<Response<string>> Handle(InitializeDistrictsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _districtCommands.CreateDistrictAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing districts.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize districts.");

        return new Response<string>()
            .WithMessage("Success initializing districts.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
