// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Regions.Interfaces;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Regions;

[ApiController]
[Route("api/geography/administrative/regions")]
[Authorize(Roles = "Administrator")]
public class RegionCommandsController : BaseApiController
{
    private readonly IRegionCommands _regionCommands;
    
    public RegionCommandsController(IRegionCommands RegionCommands)
    {
        _regionCommands = RegionCommands;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography region", Description = "Creates a new geography region in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "region created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid region data")]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto dto, CancellationToken token)
    {
        var result = await _regionCommands.CreateRegionAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography regions", Description = "Initializes multiple geography regions in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "regions initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Region data")]
    public async Task<IActionResult> InitializeRegion([FromBody] List<CreateRegionDto> dto, CancellationToken token)
    {
        var result = await _regionCommands.InitializeRegionsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography Region", Description = "Updates an existing geography Region by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Region updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Region not found")]
    public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] UpdateRegionDto dto, CancellationToken token)
    {
        var result = await _regionCommands.UpdateRegionAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography region by ID", Description = "Deletes a specific geography region by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Region deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Region not found")]
    public async Task<IActionResult> DeleteRegion(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _regionCommands.DeleteRegionAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}