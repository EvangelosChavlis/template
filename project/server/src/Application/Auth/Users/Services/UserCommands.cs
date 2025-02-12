// source
using server.src.Application.Auth.Users.Commands;
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Common.Services;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Common.Dtos;


namespace server.src.Application.Auth.Users.Services;

public class UserCommands : IUserCommands
{
    private readonly RequestExecutor _requestExecutor;

    public UserCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeUsersAsync(List<CreateUserDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeUsersCommand(dto);
        return await _requestExecutor.Execute<InitializeUsersCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> RegisterUserAsync(CreateUserDto dto, 
        bool registered, CancellationToken token = default)
    {
        var command = new RegisterUserCommand(dto, registered);
        return await _requestExecutor.Execute<RegisterUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateUserAsync(Guid id, 
        UpdateUserDto dto, CancellationToken token = default)
    {
        var command = new UpdateUserCommand(id, dto);
        return await _requestExecutor.Execute<UpdateUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ForgotPasswordAsync(ForgotPasswordDto dto, 
        CancellationToken token = default)
    {
        var command = new ForgotPasswordCommand(dto);
        return await _requestExecutor.Execute<ForgotPasswordCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ResetPasswordAsync(ResetPasswordDto dto, 
        CancellationToken token = default)
    {
        var command = new ResetPasswordCommand(dto);
        return await _requestExecutor.Execute<ResetPasswordCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> GeneratePasswordAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new GeneratePasswordCommand(id, version);
        return await _requestExecutor.Execute<GeneratePasswordCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> Enable2FAAsync(Enable2FADto dto, 
        CancellationToken token = default)
    {
        var command = new Enable2FACommand(dto);
        return await _requestExecutor.Execute<Enable2FACommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> Verify2FAAsync(Guid id, string authToken,
        Guid version, CancellationToken token = default)
    {
        var command = new Verify2FACommand(id, authToken, version);
        return await _requestExecutor.Execute<Verify2FACommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> RefreshTokenAsync(string authToken,
        CancellationToken token = default)
    {
        var command = new RefreshTokenCommand(authToken);
        return await _requestExecutor.Execute<RefreshTokenCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new ActivateUserCommand(id, version);
        return await _requestExecutor.Execute<ActivateUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateUserCommand(id, version);
        return await _requestExecutor.Execute<DeactivateUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> LockUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new LockUserCommand(id, version);
        return await _requestExecutor.Execute<LockUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UnlockUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new UnlockUserCommand(id, version);
        return await _requestExecutor.Execute<UnlockUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ConfirmEmailUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new ConfirmEmailUserCommand(id, version);
        return await _requestExecutor.Execute<ConfirmEmailUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ConfirmPhoneNumberUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new ConfirmPhoneNumberUserCommand(id, version);
        return await _requestExecutor.Execute<ConfirmPhoneNumberUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ConfirmMobilePhoneNumberUserAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new ConfirmMobilePhoneNumberUserCommand(id, version);
        return await _requestExecutor.Execute<ConfirmMobilePhoneNumberUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteUserAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteUserCommand(id, version);
        return await _requestExecutor.Execute<DeleteUserCommand, Response<string>>(command, token);
    }
}