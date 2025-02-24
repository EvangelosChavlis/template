// packages
using System.Net;
using System.Text;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.States.Dtos;

namespace server.src.Application.Geography.Administrative.States.Commands;

public record InitializeStatesCommand(List<CreateStateDto> Dto) : IRequest<Response<string>>;

public class InitializeStatesHandler : IRequestHandler<InitializeStatesCommand, Response<string>>
{
    private readonly IStateCommands _stateCommands;

    public InitializeStatesHandler(IStateCommands stateCommands)
    {
        _stateCommands = stateCommands;
    }

    public async Task<Response<string>> Handle(InitializeStatesCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _stateCommands.CreateStateAsync(item, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        if (!success)
            return new Response<string>()
                .WithMessage("Error initializing states.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize states.");

        return new Response<string>()
            .WithMessage("Success initializing states.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}
