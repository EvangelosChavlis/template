// source
using server.src.Application.Auth.UserLogouts.Commands;
using server.src.Application.Auth.UserLogouts.Interfaces;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Auth.UserLogins.Services;

public class UserLogoutCommands : IUserLogoutCommands
{
    private readonly RequestExecutor _requestExecutor;

    public UserLogoutCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> UserLogoutAsync(Guid userId, CancellationToken token = default)
    {
        var command = new UserLogoutCommand(userId);
        return await _requestExecutor.Execute<UserLogoutCommand, Response<string>>(command, token);
    }
}