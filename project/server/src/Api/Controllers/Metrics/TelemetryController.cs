// packages
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Metrics;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;

namespace server.src.WebApi.Controllers.Metrics;

[ApiController]
[Route("api/metrics/telemetry")]
public class TelemetryController : BaseApiController
{
    private readonly ITelemetryService _telemetryService;
    
    public TelemetryController(ITelemetryService telemetryService)
    {
        _telemetryService = telemetryService;
    }

    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of telemetry data", Description = "Retrieves a list of telemetry data with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of telemetry data retrieved successfully", typeof(CommandResponse<List<ListItemTelemetryDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No telemetry data found")]
    public async Task<IActionResult> GetTelemetryList([FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _telemetryService.GetTelemetryService(urlQuery, token));


    [ApiExplorerSettings(GroupName = "metrics")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get specific telemetry data by ID", Description = "Retrieves the details of a specific telemetry record by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Telemetry data retrieved successfully", typeof(ItemResponse<ItemTelemetryDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Telemetry data not found")]
    public async Task<IActionResult> GetTelemetryItem(Guid id, CancellationToken token)
        => Ok(await _telemetryService.GetTelemetryByIdService(id, token));
}
