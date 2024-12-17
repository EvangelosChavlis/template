// packages
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Application.Interfaces.Weather;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;
using server.src.Domain.Dto.Common;

namespace server.src.WebApi.Controllers.Weather;

[ApiController]
[Route("api/weather/warnings")]
public class WarningsController : BaseApiController
{
    private readonly IWarningsService _warningsService;
    
    public WarningsController(IWarningsService warningsService)
    {
        _warningsService = warningsService;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of weather warnings", Description = "Retrieves a list of weather warnings with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of warnings retrieved successfully", typeof(ListResponse<List<ListItemWarningDto>>))]
    public async Task<IActionResult> GetWarnings([FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _warningsService.GetWarningsService(urlQuery, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific weather warning by ID", Description = "Retrieves details of a specific weather warning by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning retrieved successfully", typeof(ItemResponse<ItemWarningDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warning not found")]
    public async Task<IActionResult> GetWarningById(Guid id, CancellationToken token)
        => Ok(await _warningsService.GetWarningByIdService(id, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("picker")]
    [SwaggerOperation(Summary = "Get a list of weather warnings for the picker", Description = "Retrieves a list of weather warnings available for selection in the picker.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of warnings for picker retrieved successfully", typeof(CommandResponse<List<WarningDto>>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No warnings found for picker")]
    public async Task<IActionResult> GetWarningsPicker(CancellationToken token)
        => Ok(await _warningsService.GetWarningsPickerService(token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple weather warnings", Description = "Initializes multiple weather warnings in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warnings initialized successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid warning data")]
    public async Task<IActionResult> InitializeWarning([FromBody] List<WarningDto> dto, CancellationToken token)
        => Ok(await _warningsService.InitializeWarningsService(dto, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather warning", Description = "Creates a new weather warning in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning created successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid warning data")]
    public async Task<IActionResult> CreateWarning([FromBody] WarningDto dto, CancellationToken token)
        => Ok(await _warningsService.CreateWarningService(dto, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing weather warning", Description = "Updates an existing weather warning by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning updated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warning not found")]
    public async Task<IActionResult> UpdateWarning(Guid id, [FromBody] WarningDto dto, CancellationToken token)
        => Ok(await _warningsService.UpdateWarningService(id, dto, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Delete a weather warning by ID", Description = "Deletes a specific weather warning by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warning deleted successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warning not found")]
    public async Task<IActionResult> DeleteWarning(Guid id, CancellationToken token)
        => Ok(await _warningsService.DeleteWarningService(id, token));
}
