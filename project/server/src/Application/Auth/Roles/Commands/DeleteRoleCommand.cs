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

public record DeleteRoleCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeleteRoleCommand command, CancellationToken token = default)
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
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Id == command.Id};
        var role = await _commonRepository.GetResultByIdAsync(roleFilters, roleIncludes, token);

        // Check for existence
        if (role is null)
            return new Response<string>()
                .WithMessage("Error deleting role.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Role not found.");

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

        // Check if role is already active
        if (role.IsActive)
            return new Response<string>()
                .WithMessage("Error deleting role.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Role is active.");

        // Deleting Item
        var result = await _commonRepository.DeleteAsync(role, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deleting role.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to delete role.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success deleting role.")
            .WithStatusCode((int)HttpStatusCode.NoContent)
            .WithSuccess(result)
            .WithData($"Role {role.Name} deleted successfully.");
    }
}
    