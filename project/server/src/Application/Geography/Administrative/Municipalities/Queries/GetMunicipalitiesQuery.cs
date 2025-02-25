// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Filters;
using server.src.Application.Geography.Administrative.Municipalities.Includes;
using server.src.Application.Geography.Natural.Municipalities.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Municipalities.Queries;

public record GetMunicipalitiesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemMunicipalityDto>>>;

public class GetMunicipalitiesHandler : IRequestHandler<GetMunicipalitiesQuery, ListResponse<List<ListItemMunicipalityDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetMunicipalitiesHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemMunicipalityDto>>> Handle(GetMunicipalitiesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = MunicipalityFilters.MunicipalityNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Municipality, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.MunicipalitySearchFilter();
        var filters = new Expression<Func<Municipality, bool>>[] { filter! };

        // Include related entities
        var includes = MunicipalitiesIncludes.GetMunicipalitiesIncludes();

        // Fetch paginated results
        var pagedMunicipalities = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedMunicipalities.Rows.Select(t => t.ListItemMunicipalityDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedMunicipalities.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemMunicipalityDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Municipalities fetched successfully." 
                : "No municipalities found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedMunicipalities.UrlQuery.TotalRecords,
                PageSize = pagedMunicipalities.UrlQuery.PageSize,
                PageNumber = pagedMunicipalities.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}