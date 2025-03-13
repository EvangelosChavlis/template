// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Mappings;
using server.src.Application.Geography.Administrative.Municipalities.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Municipalities.Commands;

public record CreateMunicipalityCommand(CreateMunicipalityDto Dto) : IRequest<Response<string>>;

public class CreateMunicipalityHandler : IRequestHandler<CreateMunicipalityCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMunicipalityHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateMunicipalityCommand command, CancellationToken token = default)
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
        var filters = new Expression<Func<Municipality, bool>>[] 
        { 
            m => m.Name!.Equals(command.Dto.Name) ||
                m.Code!.Equals(command.Dto.Code)
        };
        var existingMunicipality = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the municipality already exists in the system
        if (existingMunicipality is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating municipality.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Municipality with name {existingMunicipality.Name} already exists.");
        }

        // Searching Item
        var regionFilters = new Expression<Func<Region, bool>>[] { r => r.Id == command.Dto.RegionId };
        var region = await _commonRepository.GetResultByIdAsync(regionFilters, token: token);

        // Check for existence
        if (region is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating municipality.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("State not found.");
        }

        // Mapping and Saving Municipality
        var municipality = command.Dto.CreateMunicipalityModelMapping(region);
        var modelValidationResult = municipality.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(municipality, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating municipality.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create municipality.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating municipality.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Municipality {municipality.Name} inserted successfully!");
    }
}