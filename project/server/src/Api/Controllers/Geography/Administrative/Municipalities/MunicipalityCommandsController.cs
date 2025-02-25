// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Municipalities.Interfaces;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Municipalities;

[ApiController]
[Route("api/geography/administrative/municipalities")]
[Authorize(Roles = "Administrator")]
public class MunicipalityCommandsController : BaseApiController
{
    private readonly IMunicipalityCommands _municipalityCommands;
    
    public MunicipalityCommandsController(IMunicipalityCommands MunicipalityCommands)
    {
        _municipalityCommands = MunicipalityCommands;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography municipality", Description = "Creates a new geography municipality in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "municipality created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid municipality data")]
    public async Task<IActionResult> CreateMunicipality([FromBody] CreateMunicipalityDto dto, CancellationToken token)
    {
        var result = await _municipalityCommands.CreateMunicipalityAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography municipalities", Description = "Initializes multiple geography municipalities in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "municipalities initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Municipality data")]
    public async Task<IActionResult> InitializeMunicipality([FromBody] List<CreateMunicipalityDto> dto, CancellationToken token)
    {
        var result = await _municipalityCommands.InitializeMunicipalitiesAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography Municipality", Description = "Updates an existing geography Municipality by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Municipality updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Municipality not found")]
    public async Task<IActionResult> UpdateMunicipality(Guid id, [FromBody] UpdateMunicipalityDto dto, CancellationToken token)
    {
        var result = await _municipalityCommands.UpdateMunicipalityAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography municipality by ID", Description = "Deletes a specific geography municipality by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Municipality deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Municipality not found")]
    public async Task<IActionResult> DeleteMunicipality(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _municipalityCommands.DeleteMunicipalityAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}