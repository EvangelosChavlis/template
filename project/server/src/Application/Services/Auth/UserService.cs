// packages
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// source
using server.src.Application.Interfaces.Auth;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Exceptions;
using server.src.Domain.Models.Auth;
using server.src.Application.Includes.Auth;
using server.src.Domain.Models.Common;
using server.src.Application.Filters.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Auth;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<Role> _roleManager;
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, 
        RoleManager<Role> roleManager, IConfiguration configuration, DataContext context, 
        ICommonRepository commonRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemUserDto>>> GetUsersService(UrlQuery pageParams, CancellationToken token = default)
    {
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = UserFilters.UserNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<User, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.UsersSearchFilter();

        var filters = filter.UserMatchFilters();

        // Including
        var includes = UserIncludes.GetUsersIncludes();

        // Paging
        var pagedUsers = await _commonRepository.GetPagedResultsAsync(_context.Users, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedUsers.Rows.Select(a => a.ListItemUserDtoMapping()).ToList();

        // Initializing object
        return new ListResponse<List<ListItemUserDto>>()
        {
            Data = dto,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedUsers.UrlQuery.TotalRecords,
                PageSize = pagedUsers.UrlQuery.PageSize,
                PageNumber = pagedUsers.UrlQuery.PageNumber ?? 1,
            }
        };
    }


    public async Task<ItemResponse<ItemUserDto>> GetUserByIdService(string id, CancellationToken token = default)
    {
        var user = await _userManager.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, token) ??
        throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);


        var dto = user.ItemUserDtoMapping();

        return new ItemResponse<ItemUserDto>().WithData(dto);
    }

    public async Task<CommandResponse<string>> RegisterUserService(UserDto dto, bool registered)
    {
        var emailExists = await _userManager.FindByEmailAsync(dto.Email);
        if (emailExists != null)
            throw new CustomException("User with this email already exists", (int)HttpStatusCode.BadRequest);

        var userNameExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userNameExists != null)
            throw new CustomException("User with this username already exists", (int)HttpStatusCode.BadRequest);

        var user = dto.CreateUserModelMapping(registered);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description);
            throw new CustomException($"Failed to register user: {string.Join(", ", errors)}", (int)HttpStatusCode.BadRequest);
        }

        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} registered successfully");
    }

    public async Task<CommandResponse<string>> InitializeUsersService(List<UserDto> dto)
    {
        var success = false;
        var messageBuilder = new StringBuilder();
        foreach (var item in dto)
        {
            var result = await RegisterUserService(item, false);
            success = result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();


        return new CommandResponse<string>()
            .WithSuccess(success)
            .WithData(message);
    }

    public async Task<CommandResponse<AuthenticatedUserDto>> LoginUserService(UserLoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username) ??
            throw new CustomException($"User with username {dto.Username} not found", (int)HttpStatusCode.NotFound);

        var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!checkPassword)
            throw new CustomException($"Password {dto.Password} is incorrect", (int)HttpStatusCode.BadRequest);

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, isPersistent: false, 
            lockoutOnFailure: false);

        if (!result.Succeeded)
            throw new CustomException($"Failed to sign in user", (int)HttpStatusCode.BadRequest);

        var token = GenerateJwtToken(user);

        var resultDto = user.UserName!.AuthenticatedUserDtoMapping(token);

        return new CommandResponse<AuthenticatedUserDto>()
            .WithSuccess(result.Succeeded)
            .WithData(resultDto);
    }
    

    public async Task<CommandResponse<string>> LogoutUserService(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        await _signInManager.SignOutAsync();

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData($"User {user.UserName} logged out successfully");
    }

    public async Task<CommandResponse<string>> RefreshTokenService(string token)
    {
        // Implementation for refreshing JWT token
        var principal = GetPrincipalFromExpiredToken(token);
        if (principal == null)
            throw new CustomException("Invalid token.", (int)HttpStatusCode.BadRequest);

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId!);
        if (user == null)
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        var newToken = GenerateJwtToken(user);

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData(newToken);
    }

    public async Task<CommandResponse<string>> ForgotPasswordService(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // Send token via email (email service should be implemented)

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData("Password reset token sent successfully");
    }

    public async Task<CommandResponse<string>> ResetPasswordService(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);
        
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CustomException($"Failed to reset password: {errors}", (int)HttpStatusCode.BadRequest);
        }

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData("Password reset successfully");
    }

    public async Task<CommandResponse<string>> GeneratePasswordService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ??
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        var newPassword = GeneratePassword(12);

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CustomException($"Failed to reset password: {errors}", (int)HttpStatusCode.BadRequest);
        }

        user.InitialPassword = newPassword;
        var resultUpdate = await _userManager.UpdateAsync(user);

        if(!resultUpdate.Succeeded)
            throw new CustomException("Failed to update user.", (int)HttpStatusCode.BadRequest);

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData("Password generated successfully");
    }

    public async Task<CommandResponse<string>> Enable2FAService(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        // Send token via email (email service should be implemented)

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData("2FA enabled and token sent successfully");
    }

    public async Task<CommandResponse<string>> Verify2FAService(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId) ??
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", token);
        if (!isValid)
            throw new CustomException("Invalid token.", (int)HttpStatusCode.BadRequest);

        user.TwoFactorEnabled = true;
        await _userManager.UpdateAsync(user);

        return new CommandResponse<string>()
            .WithSuccess(true)
            .WithData("2FA verified successfully");
    }

    public async Task<CommandResponse<string>> UpdateUserService(string id, UserDto dto)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        dto.UpdateUserModelMapping(user);

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to update user.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} updated successfully");
    }

    public async Task<CommandResponse<string>> ActivateUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (user.IsActive)
            throw new CustomException("User is activated.", (int)HttpStatusCode.BadRequest);

        user.IsActive = true;

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to activate user.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} activated successfully");
    }

    public async Task<CommandResponse<string>> DeactivateUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (!user.IsActive)
            throw new CustomException("User is deactivated.", (int)HttpStatusCode.BadRequest);

        user.IsActive = false;

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to deactivate user.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} deactivated successfully");
    }

    public async Task<CommandResponse<string>> LockUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (user.LockoutEnabled)
            throw new CustomException("User is locked.", (int)HttpStatusCode.BadRequest);

        user.LockoutEnabled = true;

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to lock user.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} locked successfully");
    }

    public async Task<CommandResponse<string>> UnlockUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (!user.LockoutEnabled)
            throw new CustomException("User is locked.", (int)HttpStatusCode.BadRequest);

        user.LockoutEnabled = false;
        // user.DateModified = DateTime.Now;
        // user.ModifiedBy = "Autos 1-0";

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to lock unlock.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} unlocked successfully");
    }

    public async Task<CommandResponse<string>> ConfirmEmailUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (user.EmailConfirmed)
            throw new CustomException("Email is confirmed.", (int)HttpStatusCode.BadRequest);

        user.EmailConfirmed = true;

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to confirm email.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Email {user.Email} confirmed successfully");
    }

    public async Task<CommandResponse<string>> ConfirmPhoneNumberUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (user.PhoneNumberConfirmed)
            throw new CustomException("Phone number is confirmed.", (int)HttpStatusCode.BadRequest);

        user.PhoneNumberConfirmed = true;

        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to confirm phone number.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Phone number {user.PhoneNumber} confirmed successfully");
    }

    public async Task<CommandResponse<string>> ConfirmMobilePhoneNumberUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        if (user.PhoneNumberConfirmed)
            throw new CustomException("Mobile phone number is confirmed.", (int)HttpStatusCode.BadRequest);

        user.MobilePhoneNumberConfirmed = true;
        
        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to confirm mobile phone number.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Mobile phone number {user.MobilePhoneNumber} confirmed successfully");
    }

    public async Task<CommandResponse<string>> DeleteUserService(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);
        
        var result =  await _userManager.DeleteAsync(user);

        if(!result.Succeeded)
            throw new CustomException("Failed to delete user.", (int)HttpStatusCode.BadRequest);

        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"User {user.UserName} deleted successfully");
    }

    public async Task<CommandResponse<string>> AssignRoleToUserService(string userId, string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId) ?? 
            throw new CustomException("Role not found.", (int)HttpStatusCode.NotFound);

        var user = await _userManager.FindByIdAsync(userId) ?? 
            throw new CustomException("User not found.", (int)HttpStatusCode.NotFound);

        // Assign the role to the user
        var result = await _userManager.AddToRoleAsync(user, role.Name!);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CustomException($"Failed to assign role: {errors}", (int)HttpStatusCode.BadRequest);
        }

        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Role {role.Name} assigned to user {user.UserName} successfully.");
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public static string GeneratePassword(int length)
    {
        var random = new Random();
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string nonAlphanumeric = "!@#$%^&*()_-+=<>?";

        var allChars = lower + upper + digits + nonAlphanumeric;
        var password = new char[length];

        password[0] = lower[random.Next(lower.Length)];
        password[1] = upper[random.Next(upper.Length)];
        password[2] = digits[random.Next(digits.Length)];
        password[3] = nonAlphanumeric[random.Next(nonAlphanumeric.Length)];

        for (int i = 4; i < length; i++)
        {
            password[i] = allChars[random.Next(allChars.Length)];
        }

        return new string(password.OrderBy(_ => random.Next()).ToArray());
    }
}