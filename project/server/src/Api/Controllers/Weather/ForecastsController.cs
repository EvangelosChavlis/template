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
[Route("api/weather/forecasts")]
public class ForecastsController : BaseApiController
{
   private readonly IForecastsService _forecastsService;
    
    public ForecastsController(IForecastsService forecastsService)
    {
        _forecastsService = forecastsService;
    }

    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of weather forecasts", Description = "Retrieves a list of weather forecasts with optional query parameters to filter results.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of forecasts retrieved successfully", typeof(ListResponse<List<ListItemForecastDto>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid query parameters")]
    public async Task<IActionResult> GetForecasts([FromQuery] UrlQuery urlQuery, CancellationToken token)
        => Ok(await _forecastsService.GetForecastsService(urlQuery, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a specific weather forecast by ID", Description = "Retrieves details of a specific weather forecast by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast retrieved successfully", typeof(ItemResponse<ItemForecastDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Forecast not found")]
    public async Task<IActionResult> GetForecastById(Guid id, CancellationToken token)
        => Ok(await _forecastsService.GetForecastByIdService(id, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather forecast", Description = "Creates a new weather forecast entry in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast created successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid forecast data")]
    public async Task<IActionResult> CreateForecast([FromBody] ForecastDto dto, CancellationToken token)
        => Ok(await _forecastsService.CreateForecastService(dto, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPost]
    [Route("initialize")]
    [SwaggerOperation(Summary = "Initialize multiple weather forecasts", Description = "Initializes multiple weather forecasts in the system.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecasts initialized successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid forecast data")]
    public async Task<IActionResult> InitializeForecasts([FromBody] List<ForecastDto> dto, CancellationToken token)
        => Ok(await _forecastsService.InitializeForecastsService(dto, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Update an existing weather forecast", Description = "Updates an existing weather forecast by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast updated successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid forecast data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Forecast not found")]
    public async Task<IActionResult> UpdateForecast(Guid id, [FromBody] ForecastDto dto, CancellationToken token)
        => Ok(await _forecastsService.UpdateForecastService(id, dto, token));


    [ApiExplorerSettings(GroupName = "weather")]
    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Delete a weather forecast by ID", Description = "Deletes a specific weather forecast by its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Forecast deleted successfully", typeof(CommandResponse<string>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Forecast not found")]
    public async Task<IActionResult> DeleteForecast(Guid id, CancellationToken token)
        => Ok(await _forecastsService.DeleteForecastService(id, token));
}
