// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Commands;

public record DeactivateRoleCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeactivateRoleHandler : IRequestHandler<DeactivateRoleCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeactivateRoleHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeactivateRoleCommand command, CancellationToken token = default)
    {
        // Id Validation
        var idValidationResult = RoleValidators.Validate(command.Id);
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Version Validation
        var versionValidationResult = RoleValidators.Validate(command.Version);
        if (!versionValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(versionValidationResult.IsValid)
                .WithData(string.Join("\n", versionValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var roleIncludes = new Expression<Func<Role, object>>[] { };
        var roleFilters = new Expression<Func<Role, bool>>[] { r => r.Id == command.Id};
        var role = await _commonRepository.GetResultByIdAsync(roleFilters, roleIncludes, token);

        // Check for existence
        if (role is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating role.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Role not found.");
        }

        // Check for concurrency issues
        if (role.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The role has been modified by another user. Please try again.");
        }
        
        // Check if the role is not active
        if (!role.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating role.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Role is deactivated.");
        }
            
        var userRoleFilters = new Expression<Func<UserRole, bool>>[] { ur => ur.RoleId == role.Id };
        var usersWithActiveRole = await _commonRepository.GetCountAsync(userRoleFilters, token) > 0;

        if (usersWithActiveRole)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating role.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("The role is assigned to active users and cannot be deactivated.");
        }
            
        // Saving Item
        role.IsActive = false;
        var result = await _commonRepository.UpdateAsync(role, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating role.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to deactivate role.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
    
        // Initializing object
        return new Response<string>()
            .WithMessage("Error deactivating role.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Role {role.Name} deactivated successfully.");
    }
}