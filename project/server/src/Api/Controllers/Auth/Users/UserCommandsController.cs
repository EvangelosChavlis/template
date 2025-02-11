// Packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// Source
using server.src.WebApi.Controllers;
using server.src.Application.Auth.Users.Interfaces;
using server.src.Domain.Auth.Users.Dtos;

namespace server.src.Api.Controllers.Auth.Users;

[Route("api/auth/users")]
[ApiController]
public class UserCommandsController : BaseApiController
{
    private readonly IUserCommands _userCommands;

    public UserCommandsController(IUserCommands userCommands)
    {
        _userCommands = userCommands;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register a new user", Description = "Registers a new user with the provided details.")]
    [SwaggerResponse(StatusCodes.Status201Created, "User registered successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto, CancellationToken token)
    {
        var result = await _userCommands.RegisterUserAsync(dto, true, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("create")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Create a new user", Description = "Creates a user without registering.")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto, CancellationToken token)
    {
        var result = await _userCommands.RegisterUserAsync(dto, false, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("initialize")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Initialize users", Description = "Creates multiple users at once.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data")]
    public async Task<IActionResult> InitializeUsers([FromBody] List<UserDto> dto, CancellationToken token)
    {
        var result = await _userCommands.InitializeUsersAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("refresh")]
    [SwaggerOperation(Summary = "Refresh user token", Description = "Refreshes the authentication token.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Token refreshed successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid token")]
    public async Task<IActionResult> RefreshToken([FromBody] string authToken, CancellationToken token)
    {
        var result = await _userCommands.RefreshTokenAsync(authToken, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Forgot password", Description = "Sends a password reset link to the user's email.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset email sent")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto, CancellationToken token)
    {
        var result = await _userCommands.ForgotPasswordAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("reset-password")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Reset password", Description = "Resets the user's password with a valid token.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid token or password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto, CancellationToken token)
    {
        var result = await _userCommands.ResetPasswordAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("generate-password/{id}/{versionId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Generate a new password for a user", Description = "Generates a new password for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password generated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GeneratePassword(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.GeneratePasswordAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("enable-2fa")]
    [SwaggerOperation(Summary = "Enable two-factor authentication for a user")]
    public async Task<IActionResult> Enable2FA([FromBody] Enable2FADto dto, CancellationToken token)
    {
        var result = await _userCommands.Enable2FAAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("verify-2fa")]
    [SwaggerOperation(Summary = "Verify two-factor authentication for a user")]
    public async Task<IActionResult> Verify2FA([FromBody] Verify2FADto dto, CancellationToken token)
    {
        var result = await _userCommands.Verify2FAAsync(dto.UserId, dto.Token, dto.VersionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Update user details")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto dto, CancellationToken token)
    {
        var result = await _userCommands.UpdateUserAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("activate/{id}/{versionId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Activate a user")]
    public async Task<IActionResult> ActivateUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.ActivateUserAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("deactivate/{id}/{versionId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Deactivate a user")]
    public async Task<IActionResult> DeactivateUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.DeactivateUserAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("lock/{id}/{versionId}")]
    [SwaggerOperation(Summary = "Lock a user")]
    public async Task<IActionResult> LockUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.LockUserAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("unlock/{id}/{versionId}")]
    [SwaggerOperation(Summary = "Unlock a user")]
    public async Task<IActionResult> UnlockUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.UnlockUserAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("confirm/email/{id}/{versionId}")]
    [SwaggerOperation(Summary = "Confirm a user's email")]
    public async Task<IActionResult> ConfirmEmailUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.ConfirmEmailUserAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }


    [HttpDelete("{id}/{versionId}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Delete user")]
    public async Task<IActionResult> DeleteUser(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _userCommands.DeleteUserAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}