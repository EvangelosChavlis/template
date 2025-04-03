// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Includes.NaturalFeatures;
using server.src.Application.Geography.Natural.NaturalFeatures.Filters;
using server.src.Application.Geography.Natural.NaturalFeatures.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Queries;

public record GetNaturalFeaturesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemNaturalFeatureDto>>>;

public class GetNaturalFeaturesHandler : IRequestHandler<GetNaturalFeaturesQuery, ListResponse<List<ListItemNaturalFeatureDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetNaturalFeaturesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemNaturalFeatureDto>>> Handle(GetNaturalFeaturesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = NaturalFeatureFilters.NaturalFeatureNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<NaturalFeature, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.NaturalFeatureSearchFilter();
        var filters = new Expression<Func<NaturalFeature, bool>>[] { filter! };

        // Include related entities
        var includes = NaturalFeaturesIncludes.GetNaturalFeaturesIncludes();

        // Fetch paginated results
        var pagedNaturalFeatures = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedNaturalFeatures.Rows.Select(s => s.ListItemNaturalFeatureDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedNaturalFeatures.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemNaturalFeatureDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Natural features fetched successfully." 
                : "No natural features found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedNaturalFeatures.UrlQuery.TotalRecords,
                PageSize = pagedNaturalFeatures.UrlQuery.PageSize,
                PageNumber = pagedNaturalFeatures.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
