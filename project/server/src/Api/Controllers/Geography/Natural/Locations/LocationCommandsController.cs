// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Natural.Locations.Interfaces;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Natural.Locations;

[ApiController]
[Route("api/geography/natural/locations")]
[Authorize(Roles = "Administrator")]
public class LocationCommandsController : BaseApiController
{
    private readonly ILocationCommands _locationCommands;
    
    public LocationCommandsController(ILocationCommands locationCommands)
    {
        _locationCommands = locationCommands;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography location", Description = "Creates a new geography location in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Location created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid location data")]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto dto, CancellationToken token)
    {
        var result = await _locationCommands.CreateLocationAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography locations", Description = "Initializes multiple geography locations in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Locations initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid location data")]
    public async Task<IActionResult> InitializeLocation([FromBody] List<CreateLocationDto> dto, CancellationToken token)
    {
        var result = await _locationCommands.InitializeLocationsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography location", Description = "Updates an existing geography location by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Location updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Location not found")]
    public async Task<IActionResult> UpdateLocation(Guid id, [FromBody] UpdateLocationDto dto, CancellationToken token)
    {
        var result = await _locationCommands.UpdateLocationAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography location by ID", Description = "Deletes a specific geography location by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Location deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Location not found")]
    public async Task<IActionResult> DeleteLocation(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _locationCommands.DeleteLocationAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}