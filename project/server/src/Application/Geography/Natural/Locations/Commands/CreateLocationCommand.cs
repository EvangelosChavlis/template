// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Locations.Mappings;
using server.src.Application.Geography.Natural.Locations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Domain.Geography.Natural.Locations.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Locations.Commands;

public record CreateLocationCommand(CreateLocationDto Dto) : IRequest<Response<string>>;

public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLocationHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateLocationCommand command, CancellationToken token = default)
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
        var filters = new Expression<Func<Location, bool>>[] {
            l => l.Latitude == command.Dto.Latitude &&
            l.Longitude == command.Dto.Longitude &&
            l.Altitude == command.Dto.Altitude
        };
        var existingLocation = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the location already exists in the system
        if (existingLocation is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(@$"Location with coordinations 
                    ({existingLocation.GetCoordinates()}) 
                    already exists.");
        }

        // Searching Item
        var climateZoneFilters = new Expression<Func<ClimateZone, bool>>[] { c => c.Id == command.Dto.ClimateZoneId };
        var climateZone = await _commonRepository.GetResultByIdAsync(climateZoneFilters, token: token);

        // Check for existence
        if (climateZone is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Climate zone not found.");
        }

        // Searching Item
        var terrainTypeFilters = new Expression<Func<TerrainType, bool>>[] { t => t.Id == command.Dto.TerrainTypeId };
        var terrainType = await _commonRepository.GetResultByIdAsync(terrainTypeFilters, token: token);

        // Check for existence
        if (terrainType is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Terrain type not found.");
        }

        // Searching Item
        var timezoneFilters = new Expression<Func<Timezone, bool>>[] { t => t.Id == command.Dto.TimezoneId };
        var timezone = await _commonRepository.GetResultByIdAsync(timezoneFilters, token: token);

        // Check for existence
        if (timezone is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Timezone not found.");
        }

        // Mapping and Saving Location
        var location = command.Dto.CreateLocationModelMapping(
            climateZone,
            terrainType,
            timezone
        );
        var modelValidationResult = location.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(location, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create location.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating location.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData(@$"Location 
                ({location.GetCoordinates()}) 
                inserted successfully!");
    }
}