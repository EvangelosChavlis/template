// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.ClimateZones.Interfaces;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.ClimateZones;

[ApiController]
[Route("api/geography/natural/climateZones")]
[Authorize(Roles = "Administrator")]
public class ClimateZoneCommandsController : BaseApiController
{
    private readonly IClimateZoneCommands _climateZoneCommands;
    
    public ClimateZoneCommandsController(IClimateZoneCommands climatezoneCommands)
    {
        _climateZoneCommands = climatezoneCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography climate zone", Description = "Creates a new geography climate zone in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Climate zone created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid climate zone data")]
    public async Task<IActionResult> CreateClimateZone([FromBody] CreateClimateZoneDto dto, CancellationToken token)
    {
        var result = await _climateZoneCommands.CreateClimateZoneAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography climate zones", Description = "Initializes multiple geography climate zones in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Climate zones initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid climate zone data")]
    public async Task<IActionResult> InitializeClimateZone([FromBody] List<CreateClimateZoneDto> dto, CancellationToken token)
    {
        var result = await _climateZoneCommands.InitializeClimateZonesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography climate zone", Description = "Updates an existing geography climate zone by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Climate zone updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Climate zone not found")]
    public async Task<IActionResult> UpdateClimateZone(Guid id, [FromBody] UpdateClimateZoneDto dto, CancellationToken token)
    {
        var result = await _climateZoneCommands.UpdateClimateZoneAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography climate zone by ID", Description = "Deletes a specific geography climate zone by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "ClimateZone deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "ClimateZone not found")]
    public async Task<IActionResult> DeleteClimateZone(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _climateZoneCommands.DeleteClimateZoneAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}