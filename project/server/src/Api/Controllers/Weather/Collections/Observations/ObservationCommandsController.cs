// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Api.Controllers;
using server.src.Application.Weather.Collections.Observations.Interfaces;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Weather.Collections.Observations;

[ApiController]
[Route("api/weather/observations")]
[Authorize(Roles = "User")]
public class ObservationCommandsController : BaseApiController
{
    private readonly IObservationCommands _observationCommands;

    public ObservationCommandsController(IObservationCommands observationCommands)
    {
        _observationCommands = observationCommands;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather observation", Description = "Creates a new weather observation entry in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Observation created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid observation data")]
    public async Task<IActionResult> CreateObservation([FromBody] CreateObservationDto dto, CancellationToken token)
    {
        var result = await _observationCommands.CreateObservationAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple weather observations", Description = "Initializes multiple weather observations in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Observations initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid observation data")]
    public async Task<IActionResult> InitializeObservations([FromBody] List<CreateObservationDto> dto, CancellationToken token)
    {
        var result = await _observationCommands.InitializeObservationsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing weather observation", Description = "Updates an existing weather observation by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Observation updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid observation data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Observation not found")]
    public async Task<IActionResult> UpdateObservation(Guid id, [FromBody] UpdateObservationDto dto, CancellationToken token)
    {
        var result = await _observationCommands.UpdateObservationAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a weather observation by ID", Description = "Deletes a specific weather observation by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Observation deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Observation not found")]
    public async Task<IActionResult> DeleteObservation(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _observationCommands.DeleteObservationAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}