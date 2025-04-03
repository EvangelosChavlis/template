// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.SurfaceTypes;

[ApiController]
[Route("api/geography/natural/surfaceTypes")]
[Authorize(Roles = "Administrator")]
public class SurfaceTypeCommandsController : BaseApiController
{
    private readonly ISurfaceTypeCommands _surfaceTypeCommands;
    
    public SurfaceTypeCommandsController(ISurfaceTypeCommands surfaceTypeCommands)
    {
        _surfaceTypeCommands = surfaceTypeCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography SurfaceType", Description = "Creates a new geography SurfaceType in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "SurfaceType created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid SurfaceType data")]
    public async Task<IActionResult> CreateSurfaceType([FromBody] CreateSurfaceTypeDto dto, CancellationToken token)
    {
        var result = await _surfaceTypeCommands.CreateSurfaceTypeAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       
    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography SurfaceTypes", Description = "Initializes multiple geography SurfaceTypes in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "SurfaceTypes initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid SurfaceType data")]
    public async Task<IActionResult> InitializeSurfaceType([FromBody] List<CreateSurfaceTypeDto> dto, CancellationToken token)
    {
        var result = await _surfaceTypeCommands.InitializeSurfaceTypesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography SurfaceType", Description = "Updates an existing geography SurfaceType by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "SurfaceType updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "SurfaceType not found")]
    public async Task<IActionResult> UpdateSurfaceType(Guid id, [FromBody] UpdateSurfaceTypeDto dto, CancellationToken token)
    {
        var result = await _surfaceTypeCommands.UpdateSurfaceTypeAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography surface type by ID", Description = "Deletes a specific geography SurfaceType by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "SurfaceType deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "SurfaceType not found")]
    public async Task<IActionResult> DeleteSurfaceType(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _surfaceTypeCommands.DeleteSurfaceTypeAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}