// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.SurfaceTypes.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Queries;

public record GetSurfaceTypesPickerQuery() : IRequest<Response<List<PickerSurfaceTypeDto>>>;

public class GetSurfaceTypesPickerHandler : IRequestHandler<GetSurfaceTypesPickerQuery, Response<List<PickerSurfaceTypeDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetSurfaceTypesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerSurfaceTypeDto>>> Handle(GetSurfaceTypesPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<SurfaceType, bool>>[] { t => t.IsActive };
        var SurfaceTypes = await _commonRepository.GetResultPickerAsync<SurfaceType, SurfaceType>(filters, token: token);

        // Mapping
        var dto = SurfaceTypes.Select(w => w.PickerSurfaceTypeDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerSurfaceTypeDto>>()
            .WithMessage(success ? "Surface Types fetched successfully." 
                : "No surface types found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}
