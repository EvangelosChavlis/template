// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Weather.Observations.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Observations.Dtos;

namespace server.src.Api.Controllers.Weather.Observations;

[ApiController]
[Route("api/weather/observations")]
[Authorize(Roles = "User")]
public class ObservationQueriesController : BaseApiController
{
    private readonly IObservationQueries _observationQueries;
    
    public ObservationQueriesController(IObservationQueries observationQueries)
    {
        _observationQueries = observationQueries;
    }
    
    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("statistics")]
    [SwaggerOperation(Summary = "Get weather statistics", Description = "Retrieves statistical data for weather observations.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Statistics retrieved successfully", typeof(ListResponse<StatItemObservationDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request parameters")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while processing the request")]
    public async Task<IActionResult> GetObservationsStats(CancellationToken token)
    {
        var result = await _observationQueries.GetObservationsStatsAsync(token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of weather observations", Description = "Retrieves a list of weather observations with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of observations retrieved successfully", typeof(ListResponse<List<ListItemObservationDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    public async Task<IActionResult> GetObservations([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _observationQueries.GetObservationsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific weather observation by ID", Description = "Retrieves details of a specific weather observation by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Observation retrieved successfully", typeof(Response<ItemObservationDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Observation not found")]
    public async Task<IActionResult> GetObservationById(Guid id, CancellationToken token)
    {
        var result = await _observationQueries.GetObservationByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}