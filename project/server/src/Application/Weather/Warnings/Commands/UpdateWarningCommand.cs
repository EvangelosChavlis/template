// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Weather.Warnings.Mappings;
using server.src.Application.Weather.Warnings.Validators;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Commands;

public record UpdateWarningCommand(Guid Id, WarningDto Dto) : IRequest<Response<string>>;

public class UpdateWarningHandler : IRequestHandler<UpdateWarningCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateWarningHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateWarningCommand command, CancellationToken token = default)
    {
        // Validation
        var validationResult = WarningValidators.Validate(command.Dto);

        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);


        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] {  };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == command.Id};
        var warning = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        // Check for existence
        if (warning is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating warning.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Warning not found");
        }

        // Check for concurrency issues
        if (warning.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The warning has been modified by another user. Please try again.");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateWarningMapping(warning);
        var modelValidationResult = WarningValidators.Validate(warning);
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

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating warning.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update warning.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating warning.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} updated successfully!");
    }
}