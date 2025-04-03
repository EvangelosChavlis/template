// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Warnings.Mappings;
using server.src.Application.Weather.Collections.Warnings.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Warnings.Dtos;
using server.src.Domain.Weather.Collections.Warnings.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.Warnings.Commands;

public record CreateWarningCommand(CreateWarningDto Dto) : IRequest<Response<string>>;

public class CreateWarningHandler : IRequestHandler<CreateWarningCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWarningHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateWarningCommand command, CancellationToken token = default)
    {
        // Dto Validation
        var dtoValidationResult = command.Dto.Validate();
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Name!.Equals(command.Dto.Name) };
        var existingWarning = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the warning already exists in the system
        if (existingWarning is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating warning.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Warning with name {existingWarning.Name} already exists.");
        }

        // Mapping and Saving Warning
        var warning = command.Dto.CreateWarningModelMapping();
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
        var result = await _commonRepository.AddAsync(warning, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating warning.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create warning.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating warning.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} inserted successfully!");
    }
}