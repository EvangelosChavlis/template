// packages
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// source
using server.src.Api.Controllers;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Domain.Weather.Forecasts.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Api.Controllers.Weather.Forecasts;

[ApiController]
[Route("api/weather/forecasts")]
[Authorize(Roles = "User")]
public class ForecastCommandsController : BaseApiController
{
    private readonly IForecastCommands _forecastCommands;

    public ForecastCommandsController(IForecastCommands forecastCommands)
    {
        _forecastCommands = forecastCommands;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather forecast", Description = "Creates a new weather forecast entry in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast created successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid forecast data")]
    public async Task<IActionResult> CreateForecast([FromBody] CreateForecastDto dto, CancellationToken token)
    {
        var result = await _forecastCommands.CreateForecastAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple weather forecasts", Description = "Initializes multiple weather forecasts in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecasts initialized successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid forecast data")]
    public async Task<IActionResult> InitializeForecasts([FromBody] List<CreateForecastDto> dto, CancellationToken token)
    {
        var result = await _forecastCommands.InitializeForecastsAsync(dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing weather forecast", Description = "Updates an existing weather forecast by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast updated successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid forecast data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Forecast not found")]
    public async Task<IActionResult> UpdateForecast(Guid id, [FromBody] UpdateForecastDto dto, CancellationToken token)
    {
        var result = await _forecastCommands.UpdateForecastAsync(id, dto, token);
        return StatusCode(result.StatusCode, result);
    }


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpDelete]
    [Route("{id}/{versionId}")]
    [SwaggerOperation(Summary = "Delete a weather forecast by ID", Description = "Deletes a specific weather forecast by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast deleted successfully", typeof(Response<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Forecast not found")]
    public async Task<IActionResult> DeleteForecast(Guid id, Guid versionId, CancellationToken token)
    {
        var result = await _forecastCommands.DeleteForecastAsync(id, versionId, token);
        return StatusCode(result.StatusCode, result);
    }
}