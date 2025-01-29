// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.UserRoles.Validators;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

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
        var userIdValidationResult = UserRoleValidators.Validate(command.UserId);
        if (!userIdValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(userIdValidationResult.IsValid)
                .WithData(string.Join("\n", userIdValidationResult.Errors));

        // RoleId Validation
        var roleIdValidationResult = UserRoleValidators.Validate(command.RoleId);
        if (!roleIdValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(roleIdValidationResult.IsValid)
                .WithData(string.Join("\n", roleIdValidationResult.Errors));

        // Begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var roleIncludes = new Expression<Func<Role, object>>[] { };
        var roleFilters = new Expression<Func<Role, bool>>[] { r => r.Id == command.RoleId };
        var role = await _commonRepository.GetResultByIdAsync(roleFilters, roleIncludes, token);

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
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { u => u.Id == command.UserId };
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);
            
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
        var userRoleIncludes = new Expression<Func<UserRole, object>>[] { };
        var userRoleFilters = new Expression<Func<UserRole, bool>>[] 
        { 
            ur => ur.UserId == command.UserId, 
            ur => ur.RoleId == command.RoleId
        };
        var userRole = await _commonRepository.GetResultByIdAsync(userRoleFilters, userRoleIncludes, token);    

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