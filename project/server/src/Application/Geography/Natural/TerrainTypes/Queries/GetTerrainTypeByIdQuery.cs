// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.TerrainTypes.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.TerrainTypes.Queries;

public record GetTerrainTypeByIdQuery(Guid Id) : IRequest<Response<ItemTerrainTypeDto>>;

public class GetTerrainTypeByIdHandler : IRequestHandler<GetTerrainTypeByIdQuery, Response<ItemTerrainTypeDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTerrainTypeByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemTerrainTypeDto>> Handle(GetTerrainTypeByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemTerrainTypeDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemTerrainTypeDtoMapper
                    .ErrorItemTerrainTypeDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<TerrainType, bool>>[] { w => w.Id == query.Id };
        var terrainType = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (terrainType is null)
            return new Response<ItemTerrainTypeDto>()
                .WithMessage("Terrain type not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemTerrainTypeDtoMapper
                    .ErrorItemTerrainTypeDtoMapping());

        // Mapping
        var dto = terrainType.ItemTerrainTypeDtoMapping();

        // Initializing object
        return new Response<ItemTerrainTypeDto>()
            .WithMessage("Terrain type fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
