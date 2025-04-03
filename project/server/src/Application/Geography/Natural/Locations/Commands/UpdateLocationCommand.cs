// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.Locations.Mappings;
using server.src.Application.Geography.Natural.Locations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Extensions;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.Locations.Commands;

public record UpdateLocationCommand(Guid Id, UpdateLocationDto Dto) : IRequest<Response<string>>;

public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateLocationHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateLocationCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating location.")
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
        var filters = new Expression<Func<Location, bool>>[] { t => t.Id == command.Id};
        var location = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (location is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Location not found");
        }

        // Check for concurrency issues
        if (location.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The location has been modified by another {location.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (location.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The surface type has been modified by another user. Please try again.");
        }

         // Searching Item
        var climateZoneFilters = new Expression<Func<ClimateZone, bool>>[] { c => c.Id == command.Dto.ClimateZoneId };
        var climateZone = await _commonRepository.GetResultByIdAsync(climateZoneFilters, token: token);

        // Check for existence
        if (climateZone is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Climate zone not found.");
        }

        // Searching Item
        var surfaceTypeFilters = new Expression<Func<SurfaceType, bool>>[] { t => t.Id == command.Dto.SurfaceTypeId };
        var surfaceType = await _commonRepository.GetResultByIdAsync(surfaceTypeFilters, token: token);

        // Check for existence
        if (surfaceType is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Surface type not found.");
        }

        // Searching Item
        var timezoneFilters = new Expression<Func<Timezone, bool>>[] { t => t.Id == command.Dto.TimezoneId };
        var timezone = await _commonRepository.GetResultByIdAsync(timezoneFilters, token: token);

        // Check for existence
        if (timezone is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Timezone not found.");
        }

        // Searching Item
        var naturalFeatureFilters = new Expression<Func<NaturalFeature, bool>>[] { nf => nf.Id == command.Dto.TimezoneId };
        var naturalFeature = await _commonRepository.GetResultByIdAsync(naturalFeatureFilters, token: token);

        // Check for existence
        if (naturalFeature is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Natural feature not found.");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateLocationMapping(
            location,
            climateZone,
            naturalFeature,
            surfaceType,
            timezone
        );
        var modelValidationResult = location.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(location, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating location.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update location.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Location>(location.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating location.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update location.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating location.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData(@$"Location 
                {location.GetCoordinates()} 
                updated successfully!");
    }
}