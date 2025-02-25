// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Regions.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;

namespace server.src.Application.Geography.Administrative.Regions.Commands;

public record InitializeRegionsCommand(List<CreateRegionDto> Dto) : IRequest<Response<string>>;


public class InitializeRegionsHandler : IRequestHandler<InitializeRegionsCommand, Response<string>>
{
    private readonly IRegionCommands _regionCommands;

    public InitializeRegionsHandler(IRegionCommands regionCommands)
    {
        _regionCommands = regionCommands;
    }

    public async Task<Response<string>> Handle(InitializeRegionsCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _regionCommands.CreateRegionAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing regions.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize regions.");

        return new Response<string>()
            .WithMessage("Success initializing regions.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
