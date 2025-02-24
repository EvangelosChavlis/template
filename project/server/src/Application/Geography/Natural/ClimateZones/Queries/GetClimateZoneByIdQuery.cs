// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.ClimateZones.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.ClimateZones.Queries;

public record GetClimateZoneByIdQuery(Guid Id) : IRequest<Response<ItemClimateZoneDto>>;

public class GetClimateZoneByIdHandler : IRequestHandler<GetClimateZoneByIdQuery, Response<ItemClimateZoneDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetClimateZoneByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemClimateZoneDto>> Handle(GetClimateZoneByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemClimateZoneDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemClimateZoneDtoMapper
                    .ErrorItemClimateZoneDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<ClimateZone, bool>>[] { c => c.Id == query.Id };
        var climateZone = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (climateZone is null)
            return new Response<ItemClimateZoneDto>()
                .WithMessage("Climate zone not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemClimateZoneDtoMapper
                    .ErrorItemClimateZoneDtoMapping());

        // Mapping
        var dto = climateZone.ItemClimateZoneDtoMapping();

        // Initializing object
        return new Response<ItemClimateZoneDto>()
            .WithMessage("Climate zone fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
