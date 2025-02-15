// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Weather.MoonPhases.Interfaces;
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Weather.MoonPhases;

[ApiController]
[Route("api/weather/moonPhases")]
[Authorize(Roles = "Administrator")]
public class MoonPhaseCommandsController : BaseApiController
{
    private readonly IMoonPhaseCommands _moonphaseCommands;
    
    public MoonPhaseCommandsController(IMoonPhaseCommands moonphaseCommands)
    {
        _moonphaseCommands = moonphaseCommands;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather moonphase", Description = "Creates a new weather moonphase in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "MoonPhase created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid moonphase data")]
    public async Task<IActionResult> CreateMoonPhase([FromBody] CreateMoonPhaseDto dto, CancellationToken token)
    {
        var result = await _moonphaseCommands.CreateMoonPhaseAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple weather moonphases", Description = "Initializes multiple weather moonphases in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "MoonPhases initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid moonphase data")]
    public async Task<IActionResult> InitializeMoonPhase([FromBody] List<CreateMoonPhaseDto> dto, CancellationToken token)
    {
        var result = await _moonphaseCommands.InitializeMoonPhasesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing weather moonphase", Description = "Updates an existing weather moonphase by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "MoonPhase updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "MoonPhase not found")]
    public async Task<IActionResult> UpdateMoonPhase(Guid id, [FromBody] UpdateMoonPhaseDto dto, CancellationToken token)
    {
        var result = await _moonphaseCommands.UpdateMoonPhaseAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a weather moonphase by ID", Description = "Deletes a specific weather moonphase by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "MoonPhase deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "MoonPhase not found")]
    public async Task<IActionResult> DeleteMoonPhase(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _moonphaseCommands.DeleteMoonPhaseAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }

}