// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.MoonPhases.Mappings;
using server.src.Application.Weather.MoonPhases.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.MoonPhases.Commands;

public record CreateMoonPhaseCommand(CreateMoonPhaseDto Dto) : IRequest<Response<string>>;

public class CreateMoonPhaseHandler : IRequestHandler<CreateMoonPhaseCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMoonPhaseHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateMoonPhaseCommand command, CancellationToken token = default)
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
        var filters = new Expression<Func<MoonPhase, bool>>[] { x => x.Name!.Equals(command.Dto.Name) };
        var existingMoonPhase = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the moon phase already exists in the system
        if (existingMoonPhase is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating moon phase.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Moon phase with name {existingMoonPhase.Name} already exists.");
        }

        // Mapping and Saving MoonPhase
        var moonPhase = command.Dto.CreateMoonPhaseModelMapping();
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
        var result = await _commonRepository.AddAsync(moonPhase, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating moonPhase.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create moonPhase.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating moonPhase.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Moon phase {moonPhase.Name} inserted successfully!");
    }
}