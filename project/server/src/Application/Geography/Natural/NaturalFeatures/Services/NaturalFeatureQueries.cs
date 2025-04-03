// source
using server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;
using server.src.Application.Geography.Natural.NaturalFeatures.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Services;

public class NaturalFeatureQueries : INaturalFeatureQueries
{
    private readonly RequestExecutor _requestExecutor;

    public NaturalFeatureQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemNaturalFeatureDto>>> GetNaturalFeaturesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetNaturalFeaturesQuery(urlQuery);
        return await _requestExecutor
            .Execute<GetNaturalFeaturesQuery, ListResponse<List<ListItemNaturalFeatureDto>>>(query, token);
    }

    public async Task<Response<List<PickerNaturalFeatureDto>>> GetNaturalFeaturesPickerAsync(CancellationToken token = default)
    {
        var query = new GetNaturalFeaturesPickerQuery();
        return await _requestExecutor
            .Execute<GetNaturalFeaturesPickerQuery, Response<List<PickerNaturalFeatureDto>>>(query, token);
    }

    public async Task<Response<ItemNaturalFeatureDto>> GetNaturalFeatureByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetNaturalFeatureByIdQuery(id);
        return await _requestExecutor
            .Execute<GetNaturalFeatureByIdQuery, Response<ItemNaturalFeatureDto>>(query, token);
    }
}