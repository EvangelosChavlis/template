// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.Timezones.Interfaces;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.Timezones;

[ApiController]
[Route("api/geography/natural/timezones")]
[Authorize(Roles = "Administrator")]
public class TimezoneCommandsController : BaseApiController
{
    private readonly ITimezoneCommands _timezoneCommands;
    
    public TimezoneCommandsController(ITimezoneCommands timezoneCommands)
    {
        _timezoneCommands = timezoneCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography timezone", Description = "Creates a new geography timezone in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Timezone created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid timezone data")]
    public async Task<IActionResult> CreateTimezone([FromBody] CreateTimezoneDto dto, CancellationToken token)
    {
        var result = await _timezoneCommands.CreateTimezoneAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography timezones", Description = "Initializes multiple geography timezones in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Timezones initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid timezone data")]
    public async Task<IActionResult> InitializeTimezone([FromBody] List<CreateTimezoneDto> dto, CancellationToken token)
    {
        var result = await _timezoneCommands.InitializeTimezonesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography timezone", Description = "Updates an existing geography timezone by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Timezone updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Timezone not found")]
    public async Task<IActionResult> UpdateTimezone(Guid id, [FromBody] UpdateTimezoneDto dto, CancellationToken token)
    {
        var result = await _timezoneCommands.UpdateTimezoneAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography timezone by ID", Description = "Deletes a specific geography timezone by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Timezone deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Timezone not found")]
    public async Task<IActionResult> DeleteTimezone(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _timezoneCommands.DeleteTimezoneAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}