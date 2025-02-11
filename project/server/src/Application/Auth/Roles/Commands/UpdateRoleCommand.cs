// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Roles.Commands;

public record UpdateRoleCommand(
    Guid Id, 
    UpdateRoleDto Dto
) : IRequest<Response<string>>;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateRoleHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateRoleCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating role.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("User not found");

        // Id Validation
        var idValidationResult = command.Id.ValidateId();
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Validation
        var dtoValidationResult = command.Dto.Validate();
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Id == command.Id};
        var role = await _commonRepository.GetResultByIdAsync(roleFilters, token: token);

        // Check for existence
        if (role is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating role.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Role not found.");
        }

        // Check for concurrency issues
        if (role.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The role has been modified by another {role.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (role.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The role has been modified by another user. Please try again.");
        }
    
        // Check if the role is inactive
        if (!role.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating role.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Role is inactive.");
        }
            
        // Mapping, Validating, Saving Item
        command.Dto.UpdateRoleModelMapping(role);
        var modelValidationResult = role.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(role, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating role.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update role.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Role>(role.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating role.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update role.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating role.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Role {role.Name} updated successfully.");
    }
}