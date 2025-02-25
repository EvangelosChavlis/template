// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Districts;

[ApiController]
[Route("api/geography/administrative/districts")]
[Authorize(Roles = "Administrator")]
public class DistrictCommandsController : BaseApiController
{
    private readonly IDistrictCommands _districtCommands;
    
    public DistrictCommandsController(IDistrictCommands districtCommands)
    {
        _districtCommands = districtCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography district", Description = "Creates a new geography district in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "district created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid district data")]
    public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictDto dto, CancellationToken token)
    {
        var result = await _districtCommands.CreateDistrictAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography districts", Description = "Initializes multiple geography districts in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "districts initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid district data")]
    public async Task<IActionResult> InitializeDistrict([FromBody] List<CreateDistrictDto> dto, CancellationToken token)
    {
        var result = await _districtCommands.InitializeDistrictsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography district", Description = "Updates an existing geography District by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "District updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "District not found")]
    public async Task<IActionResult> UpdateDistrict(Guid id, [FromBody] UpdateDistrictDto dto, CancellationToken token)
    {
        var result = await _districtCommands.UpdateDistrictAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography district by ID", Description = "Deletes a specific geography district by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "District deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "District not found")]
    public async Task<IActionResult> DeleteDistrict(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _districtCommands.DeleteDistrictAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}