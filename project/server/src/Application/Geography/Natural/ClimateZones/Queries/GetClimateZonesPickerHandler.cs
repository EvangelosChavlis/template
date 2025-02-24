// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.ClimateZones.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.ClimateZones.Queries;

public record GetClimateZonesPickerQuery() : IRequest<Response<List<PickerClimateZoneDto>>>;

public class GetClimateZonesPickerHandler : IRequestHandler<GetClimateZonesPickerQuery, Response<List<PickerClimateZoneDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetClimateZonesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerClimateZoneDto>>> Handle(GetClimateZonesPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<ClimateZone, bool>>[] { c => c.IsActive };
        var climateZones = await _commonRepository.GetResultPickerAsync(filters, token: token);

        // Mapping
        var dto = climateZones.Select(w => w.PickerClimateZoneDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerClimateZoneDto>>()
            .WithMessage(success ? "Climate zones fetched successfully." 
                : "No Climate zones found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}
