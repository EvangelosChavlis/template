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

public record InitializeClimateZonesCommand(List<CreateClimateZoneDto> Dto) : IRequest<Response<string>>;

public class InitializeClimateZonesHandler : IRequestHandler<InitializeClimateZonesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeClimateZonesHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeClimateZonesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var climateZones = new List<ClimateZone>();
        var existingCodes = new HashSet<string>();

        var failures = new List<string>();

        foreach (var item in command.Dto)
        {
             // Dto Validation
            var dtoValidationResult = item.Validate();
            if (!dtoValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", dtoValidationResult.Errors));
                continue;
            }

            // Searching Item
            var filters = new Expression<Func<ClimateZone, bool>>[] 
            { 
                c => c.Name!.Equals(item.Name) ||
                    c.Code!.Equals(item.Code)
            };
            var existingClimateZone = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the surface type already exists in the system
            if (existingClimateZone is not null)
            {
                failures.Add($"Climate zone with name {existingClimateZone.Name} or code {existingClimateZone.Code} already exists.");
                continue;
            }

            // Mapping and Saving ClimateZone
            var climateZone = item.CreateClimateZoneModelMapping();
            var modelValidationResult = climateZone.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;                
            }

            if (existingCodes.Contains(climateZone.Code))
            {
                failures.Add($"Code {climateZone.Code} already exists in the list of climatezones.");
                continue;
            }
            
            climateZones.Add(climateZone);
            existingCodes.Add(climateZone.Code);
        }
        var result = await _commonRepository.AddRangeAsync(climateZones, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing climateZones.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize climateZones.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing climateZones.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{climateZones.Count}/{command.Dto.Count} climateZones inserted successfully!");
    }
}