// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Observations.Filters;
using server.src.Application.Weather.Collections.Observations.Mappings;
using server.src.Application.Weather.Collections.Observations.Projections;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.Observations.Queries;

public record GetObservationsQuery(UrlQuery UrlQuery) : IRequest<ListResponse<List<ListItemObservationDto>>>;

public class GetObservationsHandler : IRequestHandler<GetObservationsQuery, ListResponse<List<ListItemObservationDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetObservationsHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemObservationDto>>> Handle(GetObservationsQuery query, CancellationToken token = default)
    {
        var pageParams = query.UrlQuery;
        
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ObservationFilters.ObservationTempSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Observation, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ObservationSearchFilter();

        var filters = new Expression<Func<Observation, bool>>[] { filter! };

        // Including
        var includes = ObservationsIncludes.GetObservationsIncludes();
        // Projection
        var projection = ObservationProjections.GetObservationsProjection();
        // Paging
        var pagedObservations = await _commonRepository.GetPagedResultsAsync(
            pageParams, 
            filters, 
            includes, 
            token: token
        );
        // Mapping
        var dto = pagedObservations.Rows.Select(o => o.ListItemObservationDtoMapping()).ToList();

        // Determine success 
        var success = pagedObservations.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemObservationDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Observations fetched successfully." : 
                "No observations found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedObservations.UrlQuery.TotalRecords,
                PageSize = pagedObservations.UrlQuery.PageSize,
                PageNumber = pagedObservations.UrlQuery.PageNumber ?? 1,
            }
        };
    }
}
