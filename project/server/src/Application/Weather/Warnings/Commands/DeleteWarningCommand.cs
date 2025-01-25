// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Weather.Warnings.Validators;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Commands;

public record DeleteWarningCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeleteWarningHandler : IRequestHandler<DeleteWarningCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteWarningHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeleteWarningCommand command, CancellationToken token = default)
    {
        // Validation
        var validationResult = WarningValidators.Validate(command.Id);
        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] { };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == command.Id};
        var warning = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        // Check for existence
        if (warning is null)
            return new Response<string>()
                .WithMessage("Error deleting warning.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Warning not found.");

        // Check for concurrency issues
        if (warning.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The role has been modified by another user. Please try again.");
        }

        // Deleting Item
        var result = await _commonRepository.DeleteAsync(warning, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deleting warning.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to delete warning.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success deleting forecast.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} deleted successfully!");
    }
}