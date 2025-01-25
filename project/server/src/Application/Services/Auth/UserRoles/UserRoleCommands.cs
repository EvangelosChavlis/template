// packages
using System.Linq.Expressions;
using System.Net;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Interfaces.Auth.UserRoles;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Auth.UserRoles;

public class UserRoleCommands : IUserRoleCommands
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    
    public UserRoleCommands(DataContext context, ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
        _context = context;
    }

    public async Task<Response<string>> AssignRoleToUserService(Guid userId, Guid roleId, 
        CancellationToken token = default)
    {
        // Fetch the role using the provided roleId.
        var roleIncludes = new Expression<Func<Role, object>>[] { };
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Id == roleId };
        var role = await _commonRepository.GetResultByIdAsync(_context.Roles, roleFilters, roleIncludes, token);

        if (role == null)
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Role with id {roleId} not found.");

        if (!role.IsActive)
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Role {role.Name} is inactive.");
            
        // Fetch the user using the provided userId.
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == userId };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);
            
        if (user == null)
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {userId} not found.");

        if (!user.IsActive)
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"User {user.UserName} is inactive.");

        var userRole =  role.AssignRoleToUserMapping(user);

        // Assign the role to the user
        await _context.UserRoles.AddAsync(userRole, token);
        var result = await _context.SaveChangesAsync(token) > 0;

        // Saving failed.
        if (!result)
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to assign role to user.");

        return new Response<string>()
            .WithMessage("Success assinging role to user.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Role {role.Name} assigned to user {user.UserName} successfully.");
    }


    public async Task<Response<string>> UnassignRoleFromUserService(Guid userId, Guid roleId, 
        CancellationToken token = default)
    {
        // Fetch the role using the provided roleId.
        var roleIncludes = new Expression<Func<Role, object>>[] { };
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Id == roleId };
        var role = await _commonRepository.GetResultByIdAsync(_context.Roles, roleFilters, roleIncludes, token);

        if (role == null)
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Role with id {roleId} not found.");

            
        // Fetch the user using the provided userId.
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == userId };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);
            
        if (user == null)
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {userId} not found.");

        // Fetch the UserRole entity representing the relationship between user and role.
        var userRole = await _context.UserRoles
            .Where(ur => ur.UserId == user.Id && ur.RoleId == role.Id)
            .FirstOrDefaultAsync(token);

        if (userRole == null)
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Role is not assigned to the user.");

        // Deleting
        _context.UserRoles.Remove(userRole);
        var result = await _context.SaveChangesAsync(token) > 0;

        // Saving failed.
        if (!result)
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to unassign role from user.");

        // Return success response with relevant data.
        return new Response<string>()
            .WithMessage("Success unassinging role to user.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Role {role.Name} unassigned from user {user.UserName} successfully.");
    }
}
