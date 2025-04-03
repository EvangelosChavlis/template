// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.NaturalFeatures.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Queries;

public record GetNaturalFeatureByIdQuery(Guid Id) : IRequest<Response<ItemNaturalFeatureDto>>;

public class GetNaturalFeatureByIdHandler : IRequestHandler<GetNaturalFeatureByIdQuery, Response<ItemNaturalFeatureDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetNaturalFeatureByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemNaturalFeatureDto>> Handle(GetNaturalFeatureByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemNaturalFeatureDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemNaturalFeatureDtoMapper
                    .ErrorItemNaturalFeatureDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<NaturalFeature, bool>>[] { w => w.Id == query.Id };
        var naturalFeature = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (naturalFeature is null)
            return new Response<ItemNaturalFeatureDto>()
                .WithMessage("Natural feature not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemNaturalFeatureDtoMapper
                    .ErrorItemNaturalFeatureDtoMapping());

        // Mapping
        var dto = naturalFeature.ItemNaturalFeatureDtoMapping();

        // Initializing object
        return new Response<ItemNaturalFeatureDto>()
            .WithMessage("Natural feature fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
