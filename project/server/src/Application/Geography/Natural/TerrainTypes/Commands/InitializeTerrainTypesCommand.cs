// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.TerrainTypes.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

namespace server.src.Application.Geography.Natural.TerrainTypes.Commands;

public record InitializeTerrainTypesCommand(List<CreateTerrainTypeDto> Dto) : IRequest<Response<string>>;

public class InitializeTerrainTypesHandler : IRequestHandler<InitializeTerrainTypesCommand, Response<string>>
{
    private readonly ITerrainTypeCommands _terraintypeCommands;

    public InitializeTerrainTypesHandler(ITerrainTypeCommands terraintypeCommands)
    {
        _terraintypeCommands = terraintypeCommands;
    }

    public async Task<Response<string>> Handle(InitializeTerrainTypesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _terraintypeCommands.CreateTerrainTypeAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing terrain types.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize terrain types.");

        return new Response<string>()
            .WithMessage("Success initializing terrain types.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
