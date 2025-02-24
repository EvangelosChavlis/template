// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.ClimateZones.Interfaces;
using server.src.Domain.Common.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.ClimateZones;

[ApiController]
[Route("api/geography/natural/climateZones")]
[Authorize(Roles = "Administrator")]
public class ClimateZoneQueriesController : BaseApiController
{
    private readonly IClimateZoneQueries _climatezoneQueries;
    
    public ClimateZoneQueriesController(IClimateZoneQueries climatezoneQueries)
    {
        _climatezoneQueries = climatezoneQueries;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of geography climate zones", Description = "Retrieves a list of geography climatezones with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of climate zones retrieved successfully", typeof(ListResponse<List<ListItemClimateZoneDto>>))]
    public async Task<IActionResult> GetClimateZones([FromQuery] UrlQuery urlQuery, CancellationToken token)
    {
        var result = await _climatezoneQueries.GetClimateZonesAsync(urlQuery, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific geography climate zone by ID", Description = "Retrieves details of a specific geography climate zone by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "ClimateZone retrieved successfully", typeof(Response<ItemClimateZoneDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "ClimateZone not found")]
    public async Task<IActionResult> GetClimateZoneById(Guid id, CancellationToken token)
    {
        var result = await _climatezoneQueries.GetClimateZoneByIdAsync(id, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of geography climatezones for the picker", Description = "Retrieves a list of geography climatezones available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of climatezones for picker retrieved successfully", typeof(Response<List<PickerClimateZoneDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No climatezones found for picker")]
    public async Task<IActionResult> GetClimateZonesPicker(CancellationToken token)
    {
        var result = await _climatezoneQueries.GetClimateZonesPickerAsync(token);
        return StatusCode(result.StatusCode, result);
    }
}