// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces.Auth.UserLogouts;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Auth.UserLogouts;

public class UserLogoutCommands : IUserLogoutCommands
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    
    public UserLogoutCommands(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> LogoutUserService(Guid id, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == id };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User with not found.");

        user.SecurityStamp = Guid.NewGuid().ToString();
        var result = await _context.SaveChangesAsync(token) > 0;

        if (!result)
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("An error occurred while updating the user login status. Please try again.");

        var logout = user.UserLogoutMapping();
        await _context.UserLogouts.AddAsync(logout, token);
        var resultUserLogout = await _context.SaveChangesAsync(token) > 0;

        if (!resultUserLogout)
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("An error occurred while updating the user login status. Please try again.");

        // Return success response
        return new Response<string>()
            .WithMessage($"User {user.UserName} logged out successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData($"User {user.UserName} logged out successfully.");
    }
}