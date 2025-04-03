// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Timezones.Mappings;
using server.src.Application.Geography.Natural.Timezones.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Timezones.Commands;

public record CreateTimezoneCommand(CreateTimezoneDto Dto) : IRequest<Response<string>>;

public class CreateTimezoneHandler : IRequestHandler<CreateTimezoneCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTimezoneHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateTimezoneCommand command, CancellationToken token = default)
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
        var filters = new Expression<Func<Timezone, bool>>[] { t => t.Name!.Equals(command.Dto.Name) };
        var existingTimezone = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the surface type already exists in the system
        if (existingTimezone is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating timezone.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Timezone with name {existingTimezone.Name} already exists.");
        }

        // Mapping and Saving Timezone
        var timezone = command.Dto.CreateTimezoneModelMapping();
        var modelValidationResult = timezone.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(timezone, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating surface type.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create surface type.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating timezone.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Timezone {timezone.Name} inserted successfully!");
    }
}