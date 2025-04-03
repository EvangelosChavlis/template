// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.NaturalFeatures;

[ApiController]
[Route("api/geography/natural/naturalFeatures")]
[Authorize(Roles = "Administrator")]
public class NaturalFeatureCommandsController : BaseApiController
{
    private readonly INaturalFeatureCommands _naturalFeatureCommands;
    
    public NaturalFeatureCommandsController(INaturalFeatureCommands NaturalFeatureCommands)
    {
        _naturalFeatureCommands = NaturalFeatureCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography natural feature", Description = "Creates a new geography natural feature in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "NaturalFeature created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid natural feature data")]
    public async Task<IActionResult> CreateNaturalFeature([FromBody] CreateNaturalFeatureDto dto, CancellationToken token)
    {
        var result = await _naturalFeatureCommands.CreateNaturalFeatureAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       
    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography natural features", Description = "Initializes multiple geography natural features in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "natural features initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid natural feature data")]
    public async Task<IActionResult> InitializeNaturalFeature([FromBody] List<CreateNaturalFeatureDto> dto, CancellationToken token)
    {
        var result = await _naturalFeatureCommands.InitializeNaturalFeaturesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography natural feature", Description = "Updates an existing geography natural feature by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "natural feature updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "natural feature not found")]
    public async Task<IActionResult> UpdateNaturalFeature(Guid id, [FromBody] UpdateNaturalFeatureDto dto, CancellationToken token)
    {
        var result = await _naturalFeatureCommands.UpdateNaturalFeatureAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }

    [ApiExplorerSettings(GroupName = "geography-natural")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography surface type by ID", Description = "Deletes a specific geography natural feature by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "natural feature deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "natural feature not found")]
    public async Task<IActionResult> DeleteNaturalFeature(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _naturalFeatureCommands.DeleteNaturalFeatureAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}