// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.SurfaceTypes.Mappings;
using server.src.Application.Geography.Natural.SurfaceTypes.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Commands;

public record UpdateSurfaceTypeCommand(Guid Id, UpdateSurfaceTypeDto Dto) : IRequest<Response<string>>;

public class UpdateSurfaceTypeHandler : IRequestHandler<UpdateSurfaceTypeCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateSurfaceTypeHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateSurfaceTypeCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating surface type.")
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

        // Dto Validation
        var validationResult = command.Dto.Validate();
        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<SurfaceType, bool>>[] { t => t.Id == command.Id};
        var surfaceType = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (surfaceType is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating surface type.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Surface Type not found");
        }

        // Check for concurrency issues
        if (surfaceType.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The surface type has been modified by another {surfaceType.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (surfaceType.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The surface type has been modified by another user. Please try again.");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateSurfaceTypeMapping(surfaceType);
        var modelValidationResult = surfaceType.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(surfaceType, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating surface type.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update surface type.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<SurfaceType>(surfaceType.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating surface type.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update surface type.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating surface type.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Surface Type {surfaceType.Name} updated successfully!");
    }
}