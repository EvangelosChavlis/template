// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.States.Interfaces;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.States;

[ApiController]
[Route("api/geography/administrative/states")]
[Authorize(Roles = "Administrator")]
public class StateCommandsController : BaseApiController
{
    private readonly IStateCommands _stateCommands;
    
    public StateCommandsController(IStateCommands StateCommands)
    {
        _stateCommands = StateCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography state", Description = "Creates a new geography state in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "state created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid state data")]
    public async Task<IActionResult> CreateState([FromBody] CreateStateDto dto, CancellationToken token)
    {
        var result = await _stateCommands.CreateStateAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography states", Description = "Initializes multiple geography states in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "states initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid State data")]
    public async Task<IActionResult> InitializeState([FromBody] List<CreateStateDto> dto, CancellationToken token)
    {
        var result = await _stateCommands.InitializeStatesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography State", Description = "Updates an existing geography State by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "State updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "State not found")]
    public async Task<IActionResult> UpdateState(Guid id, [FromBody] UpdateStateDto dto, CancellationToken token)
    {
        var result = await _stateCommands.UpdateStateAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography state by ID", Description = "Deletes a specific geography state by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "State deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "State not found")]
    public async Task<IActionResult> DeleteState(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _stateCommands.DeleteStateAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}