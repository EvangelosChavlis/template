// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Geography.Natural.Locations.Extensions;
using server.src.Application.Geography.Natural.Locations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Application.Geography.Natural.Locations.Mappings;

namespace server.src.Application.Geography.Natural.Locations.Commands;

public record InitializeLocationsCommand(List<CreateLocationDto> Dto) : IRequest<Response<string>>;

public class InitializeLocationsHandler : IRequestHandler<InitializeLocationsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeLocationsHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeLocationsCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var locations = new List<Location>();
        
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
            var filters = new Expression<Func<Location, bool>>[] {
                l => l.Latitude == item.Latitude &&
                l.Longitude == item.Longitude &&
                l.Altitude == item.Altitude
            };
            var existingLocation = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the location already exists in the system
            if (existingLocation is not null)
            {
                failures.Add(@$"Location with coordinations 
                        ({existingLocation.GetCoordinates()}) 
                        already exists.");
                continue;
            }

            // Searching Item
            var climateZoneFilters = new Expression<Func<ClimateZone, bool>>[] { c => c.Id == item.ClimateZoneId };
            var climateZone = await _commonRepository.GetResultByIdAsync(climateZoneFilters, token: token);

            // Check for existence
            if (climateZone is null)
            {
                failures.Add("Climate zone not found.");
                continue;
            }

            // Searching Item
            var surfaceTypeFilters = new Expression<Func<SurfaceType, bool>>[] { s => s.Id == item.SurfaceTypeId };
            var surfaceType = await _commonRepository.GetResultByIdAsync(surfaceTypeFilters, token: token);

            // Check for existence
            if (surfaceType is null)
            {
                failures.Add("Surface Type not found.");
                continue;
            }

            // Searching Item
            var timezoneFilters = new Expression<Func<Timezone, bool>>[] { t => t.Id == item.TimezoneId };
            var timezone = await _commonRepository.GetResultByIdAsync(timezoneFilters, token: token);

            // Check for existence
            if (timezone is null)
            {
                failures.Add("Timezone not found.");
                continue;
            }

            // Searching Item
            var naturalFeatureFilters = new Expression<Func<NaturalFeature, bool>>[] { nf => nf.Id == item.NaturalFeatureId };
            var naturalFeature = await _commonRepository.GetResultByIdAsync(naturalFeatureFilters, token: token);

            // Check for existence
            if (naturalFeature is null)
            {
                failures.Add("Natural feature not found.");
                continue;
            }

            // Mapping and Saving Location
            var location = item.CreateLocationModelMapping(
                climateZone,
                naturalFeature,
                surfaceType,
                timezone
            );
            var modelValidationResult = location.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }
            
            locations.Add(location);
        }

        var result = await _commonRepository.AddRangeAsync(locations, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing locations.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize locations.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing locations.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{locations.Count}/{command.Dto.Count} locations inserted successfully!");
    }
}
