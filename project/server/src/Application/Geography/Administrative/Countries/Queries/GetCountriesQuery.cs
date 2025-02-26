// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Filters;
using server.src.Application.Geography.Natural.Countries.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Includes;

namespace server.src.Application.Geography.Administrative.Countries.Queries;

public record GetCountriesQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemCountryDto>>>;

public class GetCountrysHandler : IRequestHandler<GetCountriesQuery, ListResponse<List<ListItemCountryDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetCountrysHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemCountryDto>>> Handle(GetCountriesQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;

        // Apply default sorting if none is specified
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = CountryFilters.CountryNameSorting;
            pageParams.SortDescending = false;
        }

        // Apply filtering based on provided parameters
        Expression<Func<Country, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.CountrySearchFilter();
        var filters = new Expression<Func<Country, bool>>[] { filter! };

        // Include related entities
        var includes = CountriesIncludes.GetCountrysIncludes();

        // Fetch paginated results
        var pagedCountrys = await _commonRepository.GetPagedResultsAsync( pageParams, 
            filters, includes, token: token);
        
        // Mapping
        var dto = pagedCountrys.Rows.Select(c => c.ListItemCountryDtoMapping()).ToList();

        // Determine if the operation was successful
        var success = pagedCountrys.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemCountryDto>>()
        {
            Data = dto,
            Success = success,
            Message = success 
                ? "Countries fetched successfully." 
                : "No countries found matching the filter criteria.",
            StatusCode = success 
                ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedCountrys.UrlQuery.TotalRecords,
                PageSize = pagedCountrys.UrlQuery.PageSize,
                PageNumber = pagedCountrys.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}