// source
using server.src.Application.Auth.Users.Commands;
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Users.Services;

public class UserCommands : IUserCommands
{
    private readonly IRequestHandler<InitializeUsersCommand, Response<string>> _initializeUsersHander;
    private readonly IRequestHandler<RegisterUserCommand, Response<string>> _registerUserHandler;
    private readonly IRequestHandler<UpdateUserCommand, Response<string>> _updateUserHandler;
    private readonly IRequestHandler<ForgotPasswordCommand, Response<string>> _forgotPasswordHandler;
    private readonly IRequestHandler<ResetPasswordCommand, Response<string>> _resetPasswordHandler;
    private readonly IRequestHandler<GeneratePasswordCommand, Response<string>> _generatePasswordCommand;
    private readonly IRequestHandler<Enable2FACommand, Response<string>> _enable2FACommand;
    private readonly IRequestHandler<Verify2FACommand, Response<string>> _verify2FACommand;
    private readonly IRequestHandler<RefreshTokenCommand, Response<string>> _refreshTokenCommand;
    private readonly IRequestHandler<ActivateUserCommand, Response<string>> _activateUserCommand;
    private readonly IRequestHandler<DeactivateUserCommand, Response<string>> _deactivateUserCommand;
    private readonly IRequestHandler<LockUserCommand, Response<string>> _lockUserCommand;
    private readonly IRequestHandler<UnlockUserCommand, Response<string>> _unlockUserCommand;
    private readonly IRequestHandler<ConfirmEmailUserCommand, Response<string>> _confirmEmailUserCommand;
    private readonly IRequestHandler<ConfirmPhoneNumberUserCommand, Response<string>> _confirmPhoneNumberUserCommand;
    private readonly IRequestHandler<ConfirmMobilePhoneNumberUserCommand, Response<string>> _confirmMobilePhoneNumberUserCommand;
    private readonly IRequestHandler<DeleteUserCommand, Response<string>> _deleteUserHandler;

    public UserCommands(
        IRequestHandler<InitializeUsersCommand, Response<string>> initializeUsersHander,
        IRequestHandler<RegisterUserCommand, Response<string>> registerUserHandler,
        IRequestHandler<UpdateUserCommand, Response<string>> updateUserHandler,
        IRequestHandler<ForgotPasswordCommand, Response<string>> forgotPasswordHandler,
        IRequestHandler<ResetPasswordCommand, Response<string>> resetPasswordHandler,
        IRequestHandler<GeneratePasswordCommand, Response<string>> generatePasswordCommand,
        IRequestHandler<Enable2FACommand, Response<string>> enable2FACommand,
        IRequestHandler<Verify2FACommand, Response<string>> verify2FACommand,
        IRequestHandler<RefreshTokenCommand, Response<string>> refreshTokenCommand,
        IRequestHandler<ActivateUserCommand, Response<string>> activateUserCommand,
        IRequestHandler<DeactivateUserCommand, Response<string>> deactivateUserCommand,
        IRequestHandler<LockUserCommand, Response<string>> lockUserCommand,
        IRequestHandler<UnlockUserCommand, Response<string>> unlockUserCommand,
        IRequestHandler<ConfirmEmailUserCommand, Response<string>> confirmEmailUserCommand,
        IRequestHandler<ConfirmPhoneNumberUserCommand, Response<string>> confirmPhoneNumberUserCommand,
        IRequestHandler<ConfirmMobilePhoneNumberUserCommand, Response<string>> confirmMobilePhoneNumberUserCommand,
        IRequestHandler<DeleteUserCommand, Response<string>> deleteUserHandler)
    {
        _initializeUsersHander = initializeUsersHander;
        _registerUserHandler = registerUserHandler;
        _updateUserHandler = updateUserHandler;
        _forgotPasswordHandler = forgotPasswordHandler;
        _resetPasswordHandler = resetPasswordHandler;
        _generatePasswordCommand = generatePasswordCommand;
        _enable2FACommand = enable2FACommand;
        _verify2FACommand = verify2FACommand;
        _refreshTokenCommand = refreshTokenCommand;
        _activateUserCommand = activateUserCommand;
        _deactivateUserCommand = deactivateUserCommand;
        _lockUserCommand = lockUserCommand;
        _unlockUserCommand = unlockUserCommand;
        _confirmEmailUserCommand = confirmEmailUserCommand;
        _confirmPhoneNumberUserCommand = confirmPhoneNumberUserCommand;
        _deleteUserHandler = deleteUserHandler;
    }

    public async Task<Response<string>> InitializeUsersAsync(List<UserDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeUsersCommand(dto);
        return await _initializeUsersHander.Handle(command, token);
    }

    public async Task<Response<string>> RegisterUserAsync(UserDto dto, bool registered,
        CancellationToken token = default)
    {
        var command = new RegisterUserCommand(dto, registered);
        return await _registerUserHandler.Handle(command, token);
    }

    public async Task<Response<string>> UpdateUserAsync(Guid id, UserDto dto, 
        CancellationToken token = default)
    {
        var command = new UpdateUserCommand(id, dto);
        return await _updateUserHandler.Handle(command, token);
    }

    public async Task<Response<string>> ForgotPasswordAsync(string email, 
        CancellationToken token = default)
    {
        var command = new ForgotPasswordCommand(email);
        return await _forgotPasswordHandler.Handle(command, token);
    }

    public async Task<Response<string>> ResetPasswordAsync(string email, string authToken, 
        string newPassword, CancellationToken token = default)
    {
        var command = new ResetPasswordCommand(email, authToken, newPassword);
        return await _resetPasswordHandler.Handle(command, token);
    }

    public async Task<Response<string>> GeneratePasswordAsync(Guid id, 
        CancellationToken token = default)
    {
        var command = new GeneratePasswordCommand(id);
        return await _generatePasswordCommand.Handle(command, token);
    }

    public async Task<Response<string>> Enable2FAAsync(Guid id, 
        CancellationToken token = default)
    {
        var command = new Enable2FACommand(id);
        return await _enable2FACommand.Handle(command, token);
    }

    public async Task<Response<string>> Verify2FAAsync(Guid id, string authToken,
        CancellationToken token = default)
    {
        var command = new Verify2FACommand(id, authToken);
        return await _verify2FACommand.Handle(command, token);
    }

    public async Task<Response<string>> RefreshTokenAsync(string authToken,
        CancellationToken token = default)
    {
        var command = new RefreshTokenCommand(authToken);
        return await _refreshTokenCommand.Handle(command, token);
    }

    public async Task<Response<string>> ActivateUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new ActivateUserCommand(id);
        return await _activateUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> DeactivateUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new DeactivateUserCommand(id);
        return await _deactivateUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> LockUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new LockUserCommand(id);
        return await _lockUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> UnlockUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new UnlockUserCommand(id);
        return await _unlockUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> ConfirmEmailUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new ConfirmEmailUserCommand(id);
        return await _confirmEmailUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> ConfirmPhoneNumberUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new ConfirmPhoneNumberUserCommand(id);
        return await _confirmPhoneNumberUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> ConfirmMobilePhoneNumberUserAsync(Guid id,
        CancellationToken token = default)
    {
        var command = new ConfirmMobilePhoneNumberUserCommand(id);
        return await _confirmMobilePhoneNumberUserCommand.Handle(command, token);
    }

    public async Task<Response<string>> DeleteUserAsync(Guid id, 
        CancellationToken token = default)
    {
        var command = new DeleteUserCommand(id);
        return await _deleteUserHandler.Handle(command, token);
    }
}