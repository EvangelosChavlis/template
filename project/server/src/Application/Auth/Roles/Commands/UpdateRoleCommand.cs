// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Roles.Commands;

public record UpdateRoleCommand(Guid Id, RoleDto Dto) : IRequest<Response<string>>;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateRoleHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateRoleCommand command, CancellationToken token = default)
    {
        // Validation
        var validationResult = RoleValidators.Validate(command.Dto);
        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var roleIncludes = new Expression<Func<Role, object>>[] { };
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Id == command.Id};
        var role = await _commonRepository.GetResultByIdAsync(roleFilters, roleIncludes, token);

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
        var modelValidationResult = RoleValidators.Validate(role);
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