// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.Collections.MoonPhases.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.MoonPhases.Commands;

public record DeactivateMoonPhaseCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeactivateMoonPhaseHandler : IRequestHandler<DeactivateMoonPhaseCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeactivateMoonPhaseHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeactivateMoonPhaseCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deactivating moon phase.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("Current user not found");

        // Id Validation
        var idValidationResult = command.Id.ValidateId();
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Version Validation
        var versionValidationResult = command.Version.ValidateId();
        if (!versionValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(versionValidationResult.IsValid)
                .WithData(string.Join("\n", versionValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var moonPhaseFilters = new Expression<Func<MoonPhase, bool>>[] { m => m.Id == command.Id};
        var moonPhase = await _commonRepository.GetResultByIdAsync(moonPhaseFilters, token: token);

        // Check for existence
        if (moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating moon phase.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Moon phase not found.");
        }

        // Check for concurrency issues
        if (moonPhase.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The moon phase has been modified by another {moonPhase.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (moonPhase.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The moonphase has been modified by another user. Please try again.");
        }
        
        // Check if the moonphase is not active
        if (!moonPhase.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating moonphase.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("MoonPhase is deactivated.");
        }
            
        var forecastFilters = new Expression<Func<Forecast, bool>>[] { f => f.MoonPhaseId == moonPhase.Id };
        var forecastsWithMoonPhase = await _commonRepository.GetCountAsync(forecastFilters, token) > 0;
        
        if (forecastsWithMoonPhase)
        {
            var forecastsCounter = await _commonRepository.GetCountAsync(forecastFilters, token);

            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating moon phase.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData($"This moon phase is used from {forecastsCounter} and cannot be deactivated.");
        }
            
        // Validating, Saving Item
        moonPhase.IsActive = false;
        moonPhase.Version = Guid.NewGuid();

        var modelValidationResult = moonPhase.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(moonPhase, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating moon phase.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update moon phase entity.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<MoonPhase>(moonPhase.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating moon phase.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock moon phase.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
    
        // Initializing object
        return new Response<string>()
            .WithMessage("Error deactivating moon phase.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Moon phase {moonPhase.Name} deactivated successfully.");
    }
}