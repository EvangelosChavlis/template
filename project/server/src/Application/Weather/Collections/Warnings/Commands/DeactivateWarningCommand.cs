// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.Collections.Warnings.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Domain.Weather.Collections.Warnings.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.Warnings.Commands;

public record DeactivateWarningCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeactivateWarningHandler : IRequestHandler<DeactivateWarningCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeactivateWarningHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeactivateWarningCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deactivating warning.")
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
        var warningFilters = new Expression<Func<Warning, bool>>[] { w => w.Id == command.Id};
        var warning = await _commonRepository.GetResultByIdAsync(warningFilters, token: token);

        // Check for existence
        if (warning is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating warning.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Warning not found.");
        }

        // Check for concurrency issues
        if (warning.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The warning has been modified by another {warning.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (warning.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The warning has been modified by another user. Please try again.");
        }
        
        // Check if the warning is not active
        if (!warning.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating warning.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Warning is deactivated.");
        }
            
        var forecastFilters = new Expression<Func<Forecast, bool>>[] { f => f.WarningId == warning.Id };
        var forecastsWithWarning = await _commonRepository.GetCountAsync(forecastFilters, token) > 0;
        
        if (forecastsWithWarning)
        {
            var forecastsCounter = await _commonRepository.GetCountAsync(forecastFilters, token);

            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating warning.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData($"This warning is used from {forecastsCounter} and cannot be deactivated.");
        }
            
        // Validating, Saving Item
        warning.IsActive = false;
        warning.Version = Guid.NewGuid();

        var modelValidationResult = warning.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(warning, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating warning.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update warning entity.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Warning>(warning.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating warning.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock warning.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
    
        // Initializing object
        return new Response<string>()
            .WithMessage("Error deactivating warning.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} deactivated successfully.");
    }
}