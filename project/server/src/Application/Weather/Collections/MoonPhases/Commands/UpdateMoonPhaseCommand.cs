// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.Collections.MoonPhases.Mappings;
using server.src.Application.Weather.Collections.MoonPhases.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.MoonPhases.Commands;

public record UpdateMoonPhaseCommand(Guid Id, UpdateMoonPhaseDto Dto) : IRequest<Response<string>>;

public class UpdateMoonPhaseHandler : IRequestHandler<UpdateMoonPhaseCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateMoonPhaseHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateMoonPhaseCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating moon phase.")
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
        var filters = new Expression<Func<MoonPhase, bool>>[] { m => m.Id == command.Id};
        var moonPhase = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating moon phase.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Moon phase not found");
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
        if (moonPhase.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The moon phase has been modified by another user. Please try again.");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateMoonPhaseMapping(moonPhase);
        var modelValidationResult = MoonPhaseModelValidators.Validate(moonPhase);
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

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating moonphase.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update moon phase.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<MoonPhase>(moonPhase.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating moon phase.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update moon phase.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating moon phase.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Moon phase {moonPhase.Name} updated successfully!");
    }
}