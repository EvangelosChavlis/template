// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces.Auth.UserLogins;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Auth.UserLogins;

public class UserLoginCommands : IUserLoginCommands
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    private readonly IAuthHelper _authHelper;

    public UserLoginCommands(DataContext context, ICommonRepository commonRepository,
        IAuthHelper authHelper)
    {
        _context = context;
        _commonRepository = commonRepository;
        _authHelper = authHelper;
    }

    public async Task<Response<AuthenticatedUserDto>> LoginUserService(UserLoginDto dto, CancellationToken token = default)
    {
        // Searching for an existing user by username
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.UserName!.Equals(dto.Username) };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        // Check if the user exists
        if (user is null)
            return new Response<AuthenticatedUserDto>()
                .WithMessage($"User with username {dto.Username} not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));

        // Check if the user account is locked
        if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.UtcNow)
        {
            return new Response<AuthenticatedUserDto>()
                .WithMessage("Your account is locked due to multiple failed login attempts. Please try again later.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        var checkPassword = _authHelper.VerifyPassword(dto.Password, user.PasswordHash);

        if (!checkPassword)
        {
            // Increment failed login attempts
            user.AccessFailedCount++;

            // Lock the account if failed attempts exceed the threshold
            if (user.AccessFailedCount >= 5)
            {
                user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(15); // Lock the account for 15 minutes
                user.LockoutEnabled = true;
            }

            var resultAccess = await _context.SaveChangesAsync(token);

            return new Response<AuthenticatedUserDto>()
                .WithMessage("Invalid password. Your account will be locked after 5 unsuccessful attempts.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        // Check if the user account is active
        if (!user.IsActive)
        {
            return new Response<AuthenticatedUserDto>()
                .WithMessage("User account is not active.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        if (user.AccessFailedCount is not 0)
        {
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            var resultUser = await _context.SaveChangesAsync(token) > 0;

            if (!resultUser)
                return new Response<AuthenticatedUserDto>()
                    .WithMessage("An error occurred while updating user's status. Please try again.")
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithSuccess(false)
                    .WithData(new AuthenticatedUserDto("", ""));
        }
       
        var loginInfo = user.UserLoginMapping();
        await _context.UserLogins.AddAsync(loginInfo, token);
        var resultUserLogin = await _context.SaveChangesAsync(token) > 0;

        if (!resultUserLogin)
            return new Response<AuthenticatedUserDto>()
                .WithMessage("An error occurred while updating the user login status. Please try again.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));

        // Generate JWT token
        var authToken = await _authHelper.GenerateJwtToken(user, token);

        // Map user and token to DTO
        var resultDto = user.UserName!.AuthenticatedUserDtoMapping(authToken);

        return new Response<AuthenticatedUserDto>()
            .WithMessage("Login successful.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(resultDto);
    }
}
