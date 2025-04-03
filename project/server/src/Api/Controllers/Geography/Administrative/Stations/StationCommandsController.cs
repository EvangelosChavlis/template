// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Geography.Administrative.Stations.Interfaces;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Geography.Administrative.Stations;

[ApiController]
[Route("api/geography/administrative/stations")]
[Authorize(Roles = "Administrator")]
public class StationCommandsController : BaseApiController
{
    private readonly IStationCommands _stationCommands;
    
    public StationCommandsController(IStationCommands StationCommands)
    {
        _stationCommands = StationCommands;
    }

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new geography station", Description = "Creates a new geography station in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "station created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid station data")]
    public async Task<IActionResult> CreateStation([FromBody] CreateStationDto dto, CancellationToken token)
    {
        var result = await _stationCommands.CreateStationAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }
       

    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple geography stations", Description = "Initializes multiple geography stations in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "stations initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Station data")]
    public async Task<IActionResult> InitializeStation([FromBody] List<CreateStationDto> dto, CancellationToken token)
    {
        var result = await _stationCommands.InitializeStationsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing geography Station", Description = "Updates an existing geography Station by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Station updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Station not found")]
    public async Task<IActionResult> UpdateStation(Guid id, [FromBody] UpdateStationDto dto, CancellationToken token)
    {
        var result = await _stationCommands.UpdateStationAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "geography-administrative")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a geography station by ID", Description = "Deletes a specific geography station by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Station deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Station not found")]
    public async Task<IActionResult> DeleteStation(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _stationCommands.DeleteStationAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}