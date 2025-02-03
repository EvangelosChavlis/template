// source
using server.src.Application.Auth.UserLogins.Commands;
using server.src.Application.Auth.UserLogins.Interfaces;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserLogins.Services;

public class UserLoginCommands : IUserLoginCommands
{
    private readonly IRequestHandler<UserLoginCommand, Response<AuthenticatedUserDto>> _userLoginHandler;

    public UserLoginCommands(
        IRequestHandler<UserLoginCommand, Response<AuthenticatedUserDto>> userLoginHandler
    )
    {
        _userLoginHandler = userLoginHandler;
    }

    public async Task<Response<AuthenticatedUserDto>> UserLoginAsync(UserLoginDto dto, 
        CancellationToken token = default)
    {
        var command = new UserLoginCommand(dto);
        return await _userLoginHandler.Handle(command, token);
    }
}