// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;
using server.src.Application.Auth.Telemetry.Interfaces;

namespace server.src.WebApi.Controllers.Metrics;

[ApiController]
[Route("api/metrics/telemetry")]
[Authorize(Roles = "Administrator")]
public class TelemetryQueriesController : BaseApiController
{
    private readonly ITelemetryQueries _telemetryQueries;
    
    public TelemetryQueriesController(ITelemetryQueries telemetryQueries)
    {
        _telemetryQueries = telemetryQueries;
    }

    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of telemetry data", Description = "Retrieves a list of telemetry data with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of telemetry data retrieved successfully", typeof(Response<List<ListItemTelemetryDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No telemetry data found")]
    public async Task<IActionResult> GetTelemetry([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _telemetryQueries.GetTelemetryAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet("user/{id}")]
    [SwaggerOperation(Summary = "Get a list of telemetry data by user id", Description = "Retrieves a list of telemetry by user id with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of telemetry data by user id retrieved successfully", typeof(ListResponse<List<ListItemTelemetryDto>>))]
    public async Task<IActionResult> GetTelemetryByUserId(Guid id, [FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _telemetryQueries.GetTelemetryByUserIdAsync(id, urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get specific telemetry data by ID", Description = "Retrieves the details of a specific telemetry record by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Telemetry data retrieved successfully", typeof(Response<ItemTelemetryDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Telemetry data not found")]
    public async Task<IActionResult> GetTelemetryItem(Guid id, CancellationToken token)
    {
        var result = await _telemetryQueries.GetTelemetryByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}
