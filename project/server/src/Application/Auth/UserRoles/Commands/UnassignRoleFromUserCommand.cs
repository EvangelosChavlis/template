// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Projections;
using server.src.Application.Auth.Users.Projections;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.UserRoles.Models;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.UserRoles.Commands;

public record UnassignRoleFromUserCommand(Guid UserId, Guid RoleId) : IRequest<Response<string>>;

public class UnassignRoleFromUserHandler : IRequestHandler<UnassignRoleFromUserCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnassignRoleFromUserHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Response<string>> Handle(UnassignRoleFromUserCommand command, CancellationToken token = default)
    {
        // UserId Validation
        var userIdValidationResult = command.UserId.ValidateId();
        if (!userIdValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(userIdValidationResult.IsValid)
                .WithData(string.Join("\n", userIdValidationResult.Errors));

        // RoleId Validation
        var roleIdValidationResult = command.RoleId.ValidateId();
        if (!roleIdValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(roleIdValidationResult.IsValid)
                .WithData(string.Join("\n", roleIdValidationResult.Errors));

        // Begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var roleFilters = new Expression<Func<Role, bool>>[] { r => r.Id == command.RoleId };
        var roleProjection = RoleProjections.AssignRoleProjection();
        var role = await _commonRepository.GetResultByIdAsync(
            roleFilters, 
            projection: roleProjection, 
            token: token
        );

        // Check for existence
        if (role is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Role with id {command.RoleId} not found.");
        }
             
        // Searching Item
        var userFilters = new Expression<Func<User, bool>>[] { u => u.Id == command.UserId };
        var userProjection = UserProjections.AssignUserProjection();
        var user = await _commonRepository.GetResultByIdAsync(
            userFilters, 
            projection: userProjection, 
            token: token
        );
            
        // Check for existence
        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {command.UserId} not found.");
        }

        // Searching Item
        var userRoleFilters = new Expression<Func<UserRole, bool>>[] 
        { 
            ur => ur.UserId == command.UserId, 
            ur => ur.RoleId == command.RoleId
        };
        var userRole = await _commonRepository.GetResultByIdAsync(userRoleFilters, token: token);    

        // Check for existence
        if (userRole is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Role is already assigned to the user.");
        }

        // Deleting Item
        var result = await _commonRepository.DeleteAsync(user, token); 

        // Saving failed.
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error unassinging role to user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to unassign role from user.");
        }
           
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success unassinging role to user.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Role {role.Name} unassigned from user {user.UserName} successfully.");
    }

}