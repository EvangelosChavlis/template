// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Auth.TelemetryRecords.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;

namespace server.src.Api.Controllers.Metrics;

[ApiController]
[Route("api/metrics/telemetry")]
[Authorize(Roles = "Administrator")]
public class TelemetryQueriesController : BaseApiController
{
    private readonly ITelemetryRecordQueries _telemetryQueries;
    
    public TelemetryQueriesController(ITelemetryRecordQueries telemetryQueries)
    {
        _telemetryQueries = telemetryQueries;
    }

    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of telemetry data", Description = "Retrieves a list of telemetry data with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of telemetry data retrieved successfully", typeof(Response<List<ListItemTelemetryRecordDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No telemetry data found")]
    public async Task<IActionResult> GetTelemetryRecords([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _telemetryQueries.GetTelemetryRecordsAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet("user/{id}")]
    [SwaggerOperation(Summary = "Get a list of telemetry data by user id", Description = "Retrieves a list of telemetry by user id with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of telemetry data by user id retrieved successfully", typeof(ListResponse<List<ListItemTelemetryRecordDto>>))]
    public async Task<IActionResult> GetTelemetryRecordByUserId(Guid id, [FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _telemetryQueries.GetTelemetryRecordByUserIdAsync(id, urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get specific telemetry data by ID", Description = "Retrieves the details of a specific telemetry record by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Telemetry data retrieved successfully", typeof(Response<ItemTelemetryRecordDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Telemetry data not found")]
    public async Task<IActionResult> GetTelemetryItem(Guid id, CancellationToken token)
    {
        var result = await _telemetryQueries.GetTelemetryRecordByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }
}
