// source
using server.src.Application.Auth.UserLogouts.Commands;
using server.src.Application.Auth.UserLogouts.Interfaces;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserLogins.Services;

public class UserLogoutCommands : IUserLogoutCommands
{
    private readonly IRequestHandler<UserLogoutCommand, Response<string>> _userLogoutHandler;

    public UserLogoutCommands(
        IRequestHandler<UserLogoutCommand, Response<string>> userLogoutHandler
    )
    {
        _userLogoutHandler = userLogoutHandler;
    }

    public async Task<Response<string>> UserLogoutAsync(Guid userId, CancellationToken token = default)
    {
        var command = new UserLogoutCommand(userId);
        return await _userLogoutHandler.Handle(command, token);
    }
}