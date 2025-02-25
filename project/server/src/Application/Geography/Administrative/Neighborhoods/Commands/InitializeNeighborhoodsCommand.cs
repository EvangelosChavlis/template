// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Commands;

public record InitializeNeighborhoodsCommand(List<CreateNeighborhoodDto> Dto) : IRequest<Response<string>>;

public class InitializeNeighborhoodsHandler : IRequestHandler<InitializeNeighborhoodsCommand, Response<string>>
{
    private readonly INeighborhoodCommands _neighborhoodCommands;

    public InitializeNeighborhoodsHandler(INeighborhoodCommands neighborhoodCommands)
    {
        _neighborhoodCommands = neighborhoodCommands;
    }

    public async Task<Response<string>> Handle(InitializeNeighborhoodsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _neighborhoodCommands.CreateNeighborhoodAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing neighborhoods.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize neighborhoods.");

        return new Response<string>()
            .WithMessage("Success initializing neighborhoods.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
