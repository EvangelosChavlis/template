// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.TerrainTypes.Interfaces;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.TerrainTypes;

[ApiController]
[Route("api/geography/natural/terrainTypes")]
[Authorize(Roles = "Administrator")]
public class TerrainTypeCommandsController : BaseApiController
{
    private readonly ITerrainTypeCommands _terraintypeCommands;
    
    public TerrainTypeCommandsController(ITerrainTypeCommands terraintypeCommands)
    {
        _terraintypeCommands = terraintypeCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography terraintype", Description = "Creates a new geography terraintype in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "TerrainType created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid terraintype data")]
    public async Task<IActionResult> CreateTerrainType([FromBody] CreateTerrainTypeDto dto, CancellationToken token)
    {
        var result = await _terraintypeCommands.CreateTerrainTypeAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       
    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography terraintypes", Description = "Initializes multiple geography terraintypes in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "TerrainTypes initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid terraintype data")]
    public async Task<IActionResult> InitializeTerrainType([FromBody] List<CreateTerrainTypeDto> dto, CancellationToken token)
    {
        var result = await _terraintypeCommands.InitializeTerrainTypesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography terraintype", Description = "Updates an existing geography terraintype by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "TerrainType updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "TerrainType not found")]
    public async Task<IActionResult> UpdateTerrainType(Guid id, [FromBody] UpdateTerrainTypeDto dto, CancellationToken token)
    {
        var result = await _terraintypeCommands.UpdateTerrainTypeAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography terrain type by ID", Description = "Deletes a specific geography terraintype by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "TerrainType deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "TerrainType not found")]
    public async Task<IActionResult> DeleteTerrainType(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _terraintypeCommands.DeleteTerrainTypeAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}