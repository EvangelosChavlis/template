// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;
using server.src.WebApi.Controllers;
using server.src.Application.Weather.Forecasts.Interfaces;

namespace server.src.Api.Controllers.Weather.Forecasts;

[ApiController]
[Route("api/weather/forecasts")]
[Authorize(Roles = "User")]
public class ForecastQueriesController : BaseApiController
{
    private readonly IForecastQueries _forecastQueries;
    
    public ForecastQueriesController(IForecastQueries forecastQueries)
    {
        _forecastQueries = forecastQueries;
    }
    
    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("statistics")]
    [SwaggerOperation(Summary = "Get weather statistics", Description = "Retrieves statistical data for weather forecasts.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Statistics retrieved successfully", typeof(ListResponse<StatItemForecastDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request parameters")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while processing the request")]
    public async Task<IActionResult> GetForecastsStats(CancellationToken token)
    {
        var result = await _forecastQueries.GetForecastsStatsAsync(token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of weather forecasts", Description = "Retrieves a list of weather forecasts with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of forecasts retrieved successfully", typeof(ListResponse<List<ListItemForecastDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    public async Task<IActionResult> GetForecasts([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _forecastQueries.GetForecastsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific weather forecast by ID", Description = "Retrieves details of a specific weather forecast by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast retrieved successfully", typeof(Response<ItemForecastDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Forecast not found")]
    public async Task<IActionResult> GetForecastById(Guid id, CancellationToken token)
    {
        var result = await _forecastQueries.GetForecastByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}