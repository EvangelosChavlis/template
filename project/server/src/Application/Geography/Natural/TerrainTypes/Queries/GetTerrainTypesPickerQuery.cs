// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.TerrainTypes.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.TerrainTypes.Queries;

public record GetTerrainTypesPickerQuery() : IRequest<Response<List<PickerTerrainTypeDto>>>;

public class GetTerrainTypesPickerHandler : IRequestHandler<GetTerrainTypesPickerQuery, Response<List<PickerTerrainTypeDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTerrainTypesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerTerrainTypeDto>>> Handle(GetTerrainTypesPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<TerrainType, bool>>[] { t => t.IsActive };
        var terrainTypes = await _commonRepository.GetResultPickerAsync(filters, token: token);

        // Mapping
        var dto = terrainTypes.Select(w => w.PickerTerrainTypeDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerTerrainTypeDto>>()
            .WithMessage(success ? "Terrain types fetched successfully." 
                : "No terrain types found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}
