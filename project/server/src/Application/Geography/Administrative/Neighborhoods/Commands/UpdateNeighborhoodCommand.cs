// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Neighborhoods.Mappings;
using server.src.Application.Geography.Administrative.Neighborhoods.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Administrative.Districts.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Commands;

public record UpdateNeighborhoodCommand(Guid Id, UpdateNeighborhoodDto Dto) : IRequest<Response<string>>;

public class UpdateNeighborhoodHandler : IRequestHandler<UpdateNeighborhoodCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateNeighborhoodHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateNeighborhoodCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating neighborhood.")
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
        var filters = new Expression<Func<Neighborhood, bool>>[] { t => t.Id == command.Id};
        var neighborhood = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (neighborhood is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Neighborhood not found");
        }

        // Check for concurrency issues
        if (neighborhood.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The neighborhood has been modified by another {neighborhood.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (neighborhood.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The surface type has been modified by another user. Please try again.");
        }

        // Searching Item
        var districtFilters = new Expression<Func<District, bool>>[] { d => d.Id == command.Dto.DistrictId };
        var district = await _commonRepository.GetResultByIdAsync(districtFilters, token: token);

        // Check for existence
        if (district is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating state.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("District not found.");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateNeighborhoodMapping(neighborhood, district);
        var modelValidationResult = neighborhood.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(neighborhood, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update neighborhood.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Neighborhood>(neighborhood.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update neighborhood.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating neighborhood.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Neighborhood {neighborhood.Name} updated successfully!");
    }
}