// packages
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

// source
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Persistence.Contexts;

namespace server.src.Application.Weather.Forecasts.Queries;

public record GetForecastsStatsQuery() : IRequest<Response<List<StatItemForecastDto>>>;

public class GetForecastsStatsHandler : IRequestHandler<GetForecastsStatsQuery, Response<List<StatItemForecastDto>>>
{
    private readonly string cacheKey = "forecastStats";
    private readonly DataContext _context;
    private readonly IMemoryCache _memoryCache;

    public GetForecastsStatsHandler(DataContext context,IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<Response<List<StatItemForecastDto>>> Handle(GetForecastsStatsQuery query, CancellationToken token = default)
    {
        // Check if data is in cache
        if (_memoryCache.TryGetValue(cacheKey, out List<StatItemForecastDto>? cachedData))
        {
            // If cached data is not empty, return it
            if (cachedData!.Count is not 0)
                return new Response<List<StatItemForecastDto>>().WithData(cachedData!);
        }

        // If data is not in cache or is empty, fetch data from the database
        var dto = await _context.Forecasts
            .OrderBy(f => f.Date)
            .Select(f => f.StatItemForecastDtoMapping())
            .ToListAsync(token);

        // Store the fetched data in cache for future requests (with a 1-hour expiration time)
        _memoryCache.Set(cacheKey, dto, TimeSpan.FromHours(1));

        // Determine success 
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<StatItemForecastDto>>()
            .WithMessage(success ? "Forecast stats fetched successfully." : 
                "No forecasts stats found.")
            .WithStatusCode(success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound)
            .WithSuccess(success)
            .WithData(dto);
    }
}
