// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Weather.Collections.Warnings.Interfaces;
using server.src.Domain.Weather.Collections.Warnings.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Weather.Collections.Warnings;

[ApiController]
[Route("api/weather/warnings")]
[Authorize(Roles = "Administrator")]
public class WarningCommandsController : BaseApiController
{
    private readonly IWarningCommands _warningCommands;
    
    public WarningCommandsController(IWarningCommands warningCommands)
    {
        _warningCommands = warningCommands;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather warning", Description = "Creates a new weather warning in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid warning data")]
    public async Task<IActionResult> CreateWarning([FromBody] CreateWarningDto dto, CancellationToken token)
    {
        var result = await _warningCommands.CreateWarningAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple weather warnings", Description = "Initializes multiple weather warnings in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warnings initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid warning data")]
    public async Task<IActionResult> InitializeWarning([FromBody] List<CreateWarningDto> dto, CancellationToken token)
    {
        var result = await _warningCommands.InitializeWarningsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing weather warning", Description = "Updates an existing weather warning by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warning not found")]
    public async Task<IActionResult> UpdateWarning(Guid id, [FromBody] UpdateWarningDto dto, CancellationToken token)
    {
        var result = await _warningCommands.UpdateWarningAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a weather warning by ID", Description = "Deletes a specific weather warning by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warning not found")]
    public async Task<IActionResult> DeleteWarning(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _warningCommands.DeleteWarningAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }

}