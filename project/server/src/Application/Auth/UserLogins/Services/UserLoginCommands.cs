// source
using server.src.Application.Auth.UserLogins.Commands;
using server.src.Application.Auth.UserLogins.Interfaces;
using server.src.Application.Common.Services;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Auth.UserLogins.Services;

public class UserLoginCommands : IUserLoginCommands
{
    private readonly RequestExecutor _requestExecutor;

    public UserLoginCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<AuthenticatedUserDto>> UserLoginAsync(UserLoginDto dto, 
        CancellationToken token = default)
    {
        var command = new UserLoginCommand(dto);
        return await _requestExecutor.Execute<UserLoginCommand, Response<AuthenticatedUserDto>>(command, token);
    }
}