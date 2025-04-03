// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.SurfaceTypes.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Queries;

public record GetSurfaceTypeByIdQuery(Guid Id) : IRequest<Response<ItemSurfaceTypeDto>>;

public class GetSurfaceTypeByIdHandler : IRequestHandler<GetSurfaceTypeByIdQuery, Response<ItemSurfaceTypeDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetSurfaceTypeByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemSurfaceTypeDto>> Handle(GetSurfaceTypeByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemSurfaceTypeDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemSurfaceTypeDtoMapper
                    .ErrorItemSurfaceTypeDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<SurfaceType, bool>>[] { w => w.Id == query.Id };
        var SurfaceType = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (SurfaceType is null)
            return new Response<ItemSurfaceTypeDto>()
                .WithMessage("Surface Type not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemSurfaceTypeDtoMapper
                    .ErrorItemSurfaceTypeDtoMapping());

        // Mapping
        var dto = SurfaceType.ItemSurfaceTypeDtoMapping();

        // Initializing object
        return new Response<ItemSurfaceTypeDto>()
            .WithMessage("Surface Type fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
