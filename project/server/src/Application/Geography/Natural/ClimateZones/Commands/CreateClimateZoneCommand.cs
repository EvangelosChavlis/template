// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.ClimateZones.Mappings;
using server.src.Application.Geography.Natural.ClimateZones.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.ClimateZones.Commands;

public record CreateClimateZoneCommand(CreateClimateZoneDto Dto) : IRequest<Response<string>>;

public class CreateClimateZoneHandler : IRequestHandler<CreateClimateZoneCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClimateZoneHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateClimateZoneCommand command, CancellationToken token = default)
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
        var filters = new Expression<Func<ClimateZone, bool>>[] 
        { 
            c => c.Name!.Equals(command.Dto.Name) ||
                c.Code!.Equals(command.Dto.Code)
        };
        var existingClimateZone = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the surface type already exists in the system
        if (existingClimateZone is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating climate zone.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Climate zone with name {existingClimateZone.Name} already exists.");
        }

        // Mapping and Saving ClimateZone
        var climateZone = command.Dto.CreateClimateZoneModelMapping();
        var modelValidationResult = climateZone.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(climateZone, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating climate zone.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create climate zone.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating climate zone.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Climate zone {climateZone.Name} inserted successfully!");
    }
}