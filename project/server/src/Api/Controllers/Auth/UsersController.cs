// packages
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;

namespace server.src.Api.Controllers.Auth;

[Route("api/auth/users")]
[ApiController]
public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all users", Description = "Fetches a list of all users.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved users", typeof(ListResponse<ListItemUserDto>))]
    public async Task<IActionResult> GetUsers([FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _userService.GetUsersService(urlQuery, token));


    [HttpPost]
    [Route("register")]
    [SwaggerOperation(Summary = "Register a new user", Description = "Registers a new user with the provided details.")]
    [SwaggerResponse(StatusCodes.Status201Created, "User registered successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data")]
    public async Task<IActionResult> Register([FromBody] UserDto dto)
        => Ok(await _userService.RegisterUserService(dto, true));

    
    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("create")]
    [SwaggerOperation(Summary = "Create a new user", Description = "Creates a user without registering.")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        => Ok(await _userService.RegisterUserService(dto, false));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize users", Description = "Creates multiple users at once.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users initialized successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data")]
    public async Task<IActionResult> InitializeUsers([FromBody] List<UserDto> dto)
        => Ok(await _userService.InitializeUsersService(dto));


    [HttpPost]
    [Route("login")]
    [SwaggerOperation(Summary = "User login", Description = "Logs in a user with the provided credentials.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged in successfully", typeof(CommandResponse<AuthenticatedUserDto>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        => Ok(await _userService.LoginUserService(dto));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("logout")]
    [SwaggerOperation(Summary = "User logout", Description = "Logs out the current user.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged out successfully")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _userService.LogoutUserService(userId!));
    }


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("refresh")]
    [SwaggerOperation(Summary = "Refresh user token", Description = "Refreshes the authentication token.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Token refreshed successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid token")]
    public async Task<IActionResult> RefreshToken([FromBody] string token)
        => Ok(await _userService.RefreshTokenService(token));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("forgot-password")]
    [SwaggerOperation(Summary = "Forgot password", Description = "Sends a password reset link to the user's email.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset email sent")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        => Ok(await _userService.ForgotPasswordService(dto.Email));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("reset-password")]
    [SwaggerOperation(Summary = "Reset password", Description = "Resets the user's password with a valid token.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid token or password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        => Ok(await _userService.ResetPasswordService(dto.Email, dto.Token, dto.NewPassword));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("generate-password/{id}")]
    [SwaggerOperation(Summary = "Generate a new password for a user", Description = "Generates a new password for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password generated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid password generation request")]
    public async Task<IActionResult> GeneratePassword(string id)
        => Ok(await _userService.GeneratePasswordService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("enable-2fa")]
    [SwaggerOperation(Summary = "Enable two-factor authentication for a user", Description = "Enables 2FA for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Two-factor authentication enabled successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request for enabling 2FA")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> Enable2FA([FromBody] Enable2FADto dto)
        => Ok(await _userService.Enable2FAService(dto.UserId));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPost]
    [Route("verify-2fa")]
    [SwaggerOperation(Summary = "Verify two-factor authentication for a user", Description = "Verifies the 2FA token for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Two-factor authentication verified successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid 2FA token or request")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid 2FA token")]
    public async Task<IActionResult> Verify2FA([FromBody] Verify2FADto dto)
        => Ok(await _userService.Verify2FAService(dto.UserId, dto.Token));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get user by ID", Description = "Fetches a user by their ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User retrieved successfully", typeof(UserDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetUserById(string id)
        => Ok(await _userService.GetUserByIdService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update user details", Description = "Updates an existing user's information using their ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User updated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user data or update request")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto dto)
        => Ok(await _userService.UpdateUserService(id, dto));

    
    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("activate/{id}")]
    [SwaggerOperation(Summary = "Activate a user", Description = "Activates a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User activated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> ActivateUser(string id)
        => Ok(await _userService.ActivateUserService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("deactivate/{id}")]
    [SwaggerOperation(Summary = "Deactivate a user", Description = "Deactivates a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User deactivated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> DeactivateUser(string id)
        => Ok(await _userService.DeactivateUserService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("lock/{id}")]
    [SwaggerOperation(Summary = "Lock a user", Description = "Locks a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User locked successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> LockUser(string id)
        => Ok(await _userService.LockUserService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("unlock/{id}")]
    [SwaggerOperation(Summary = "Unlock a user", Description = "Unlocks a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User unlocked successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> UnlockUser(string id)
        => Ok(await _userService.UnlockUserService(id));



    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("confirm/email/{id}")]
    [SwaggerOperation(Summary = "Confirm a user's email", Description = "Confirms the email address for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Email confirmed successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid email confirmation request")]
    public async Task<IActionResult> ConfirmEmailUser(string id)
        => Ok(await _userService.ConfirmEmailUserService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("confirm/phoneNumber/{id}")]
    [SwaggerOperation(Summary = "Confirm a user's phone number", Description = "Confirms the phone number for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Phone number confirmed successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid phone number confirmation request")]
    public async Task<IActionResult> ConfirmPhoneNumberUser(string id)
        => Ok(await _userService.ConfirmPhoneNumberUserService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("confirm/mobilePhoneNumber/{id}")]
    [SwaggerOperation(Summary = "Confirm a user's mobile phone number", Description = "Confirms the mobile phone number for a specific user by their unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Mobile phone number confirmed successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid mobile phone confirmation request")]
    public async Task<IActionResult> ConfirmMobilePhoneNumberUser(string id)
        => Ok(await _userService.ConfirmMobilePhoneNumberUserService(id));


    [ApiExplorerSettings(GroupName = "auth")]
    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Delete user", Description = "Deletes a user by their ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User deleted successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> DeleteUser(string id)
        => Ok(await _userService.DeleteUserService(id));

    
    [ApiExplorerSettings(GroupName = "auth")]
    [HttpGet]
    [Route("assign/{userId}/{roleId}")]
    [SwaggerOperation(Summary = "Assign a role to a user", Description = "Assigns a specific role to a user by their unique user ID and role ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role assigned to user successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User or role not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid assignment request")]
    public async Task<IActionResult> AssignRoleToUser(string userId, string roleId)
        => Ok(await _userService.AssignRoleToUserService(userId, roleId));
}
