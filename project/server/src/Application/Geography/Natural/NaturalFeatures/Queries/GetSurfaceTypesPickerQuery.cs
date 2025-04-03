// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.NaturalFeatures.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Queries;

public record GetNaturalFeaturesPickerQuery() : IRequest<Response<List<PickerNaturalFeatureDto>>>;

public class GetNaturalFeaturesPickerHandler : IRequestHandler<GetNaturalFeaturesPickerQuery, Response<List<PickerNaturalFeatureDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetNaturalFeaturesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerNaturalFeatureDto>>> Handle(GetNaturalFeaturesPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<NaturalFeature, bool>>[] { t => t.IsActive };
        var NaturalFeatures = await _commonRepository.GetResultPickerAsync<NaturalFeature, NaturalFeature>(filters, token: token);

        // Mapping
        var dto = NaturalFeatures.Select(w => w.PickerNaturalFeatureDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerNaturalFeatureDto>>()
            .WithMessage(success ? "Natural features fetched successfully." 
                : "No natural features found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}
