// packages
using System.Net;
using System.Text;
using server.src.Application.Auth.Users.Interfaces;

// source
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Users.Commands;

public record InitializeUsersCommand(List<UserDto> Dto) : IRequest<Response<string>>;

public class InitializeUsersHandler : IRequestHandler<InitializeUsersCommand, Response<string>>
{
    private readonly IUserCommands _userCommands;

    public InitializeUsersHandler(IUserCommands userCommands)
    {
        _userCommands = userCommands;
    }

    public async Task<Response<string>> Handle(InitializeUsersCommand command, CancellationToken token = default)
    {
        var success = true;
        var messageBuilder = new StringBuilder();

        foreach (var item in command.Dto)
        {
            var result = await _userCommands.RegisterUserAsync(item, false, token);
            success &= result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();

        // Saving failed.
        if(!success)
            return new Response<string>()
                .WithMessage("Error initializing users.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(success)
                .WithData("Failed to initialize users.");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing users.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(success)
            .WithData(message);
    }
}