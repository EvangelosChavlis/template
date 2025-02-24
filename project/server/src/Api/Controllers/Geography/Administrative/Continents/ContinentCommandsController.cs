// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Continents;

[ApiController]
[Route("api/geography/administrative/continents")]
[Authorize(Roles = "Administrator")]
public class ContinentCommandsController : BaseApiController
{
    private readonly IContinentCommands _continentCommands;
    
    public ContinentCommandsController(IContinentCommands continentCommands)
    {
        _continentCommands = continentCommands;
    }

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography continent", Description = "Creates a new geography continent in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Continent created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid continent data")]
    public async Task<IActionResult> CreateContinent([FromBody] CreateContinentDto dto, CancellationToken token)
    {
        var result = await _continentCommands.CreateContinentAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography continents", Description = "Initializes multiple geography continents in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Continents initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid continent data")]
    public async Task<IActionResult> InitializeContinent([FromBody] List<CreateContinentDto> dto, CancellationToken token)
    {
        var result = await _continentCommands.InitializeContinentsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography continent", Description = "Updates an existing geography continent by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Continent updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Continent not found")]
    public async Task<IActionResult> UpdateContinent(Guid id, [FromBody] UpdateContinentDto dto, CancellationToken token)
    {
        var result = await _continentCommands.UpdateContinentAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography continent by ID", Description = "Deletes a specific geography continent by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Continent deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Continent not found")]
    public async Task<IActionResult> DeleteContinent(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _continentCommands.DeleteContinentAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}