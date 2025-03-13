// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Projections;
using server.src.Application.Auth.UserRoles.Mappings;
using server.src.Application.Auth.UserRoles.Validators;
using server.src.Application.Auth.Users.Projections;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.UserRoles.Commands;

public record AssingRoleToUserCommand(Guid UserId, Guid RoleId) : IRequest<Response<string>>;

public class AssingRoleToUserHandler : IRequestHandler<AssingRoleToUserCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssingRoleToUserHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(AssingRoleToUserCommand command, CancellationToken token = default)
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
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Id == command.RoleId };
        var roleProjection = RoleProjections.AssignRoleProjection();
        var role = await _commonRepository.GetResultByIdAsync<Role>(
            roleFilters,
            projection: roleProjection, 
            token: token
        );

        // Check for existence
        if (role is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Role with id {command.RoleId} not found.");
        }
            
        // Check if role is inactive
        if (!role.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Role {role.Name} is inactive.");
        }
            
        // Searching Item
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.UserId };
        var userProjection = UserProjections.AssignUserProjection();
        var user = await _commonRepository.GetResultByIdAsync<User>(
            userFilters, 
            projection: userProjection, 
            token: token
        );
        
        // Check for existence
        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {command.UserId} not found.");
        }
            
        // Check if user is inactive
        if (!user.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"User {user.UserName} is inactive.");
        }

        // Mapping, Validating, Saving Item
        var userRole =  role.AssignRoleToUserMapping(user);
        var modelValidationResult = UserRoleValidators.Validate(userRole);
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(role, token);

        // Saving failed.
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error assinging role to user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to assign role to user.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success assinging role to user.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Role {role.Name} assigned to user {user.UserName} successfully.");
    }

}
