// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Neighborhoods;

[ApiController]
[Route("api/geography/administrative/neighborhoods")]
[Authorize(Roles = "Administrator")]
public class NeighborhoodCommandsController : BaseApiController
{
    private readonly INeighborhoodCommands _neighborhoodCommands;
    
    public NeighborhoodCommandsController(INeighborhoodCommands neighborhoodCommands)
    {
        _neighborhoodCommands = neighborhoodCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography region", Description = "Creates a new geography region in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "region created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid region data")]
    public async Task<IActionResult> CreateNeighborhood([FromBody] CreateNeighborhoodDto dto, CancellationToken token)
    {
        var result = await _neighborhoodCommands.CreateNeighborhoodAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography neighborhoods", Description = "Initializes multiple geography regions in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "regions initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Neighborhood data")]
    public async Task<IActionResult> InitializeNeighborhood([FromBody] List<CreateNeighborhoodDto> dto, CancellationToken token)
    {
        var result = await _neighborhoodCommands.InitializeNeighborhoodsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography neighborhood", Description = "Updates an existing geography Neighborhood by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Neighborhood updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Neighborhood not found")]
    public async Task<IActionResult> UpdateNeighborhood(Guid id, [FromBody] UpdateNeighborhoodDto dto, CancellationToken token)
    {
        var result = await _neighborhoodCommands.UpdateNeighborhoodAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography region by ID", Description = "Deletes a specific geography region by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Neighborhood deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Neighborhood not found")]
    public async Task<IActionResult> DeleteNeighborhood(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _neighborhoodCommands.DeleteNeighborhoodAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}