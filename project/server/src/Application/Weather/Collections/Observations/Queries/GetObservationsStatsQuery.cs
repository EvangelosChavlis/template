// packages
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Observations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Persistence.Common.Contexts;

namespace server.src.Application.Weather.Collections.Observations.Queries;

public record GetObservationsStatsQuery() : IRequest<Response<List<StatItemObservationDto>>>;

public class GetObservationsStatsHandler : IRequestHandler<GetObservationsStatsQuery, Response<List<StatItemObservationDto>>>
{
    private readonly string cacheKey = "observationStats";
    private readonly DataContext _context;
    private readonly IMemoryCache _memoryCache;

    public GetObservationsStatsHandler(DataContext context,IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<Response<List<StatItemObservationDto>>> Handle(GetObservationsStatsQuery query, CancellationToken token = default)
    {
        // Check if data is in cache
        if (_memoryCache.TryGetValue(cacheKey, out List<StatItemObservationDto>? cachedData))
        {
            // If cached data is not empty, return it
            if (cachedData!.Count is not 0)
                return new Response<List<StatItemObservationDto>>().WithData(cachedData!);
        }

        // If data is not in cache or is empty, fetch data from the database
        var dto = await _context.WeatherDbSets.CollectionsDbSets.Observations
            .OrderBy(f => f.Timestamp)
            .Select(f => f.StatItemObservationDtoMapping())
            .ToListAsync(token);

        // Store the fetched data in cache for future requests (with a 1-hour expiration time)
        _memoryCache.Set(cacheKey, dto, TimeSpan.FromHours(1));

        // Determine success 
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<StatItemObservationDto>>()
            .WithMessage(success ? "Observation stats fetched successfully." : 
                "No observations stats found.")
            .WithStatusCode(success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound)
            .WithSuccess(success)
            .WithData(dto);
    }
}
