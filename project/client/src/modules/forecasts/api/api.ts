// source
import { CommandResponse } from "src/models/common/commandResponse";
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { 
  ListItemForecastDto, 
  ItemForecastDto, 
  ForecastDto 
} from "src/models/weather/forecastsDto";
import { urlParams, baseUrl } from "src/utils/utils";

/**
 * Fetches a paginated list of forecasts.
 * 
 * @param {Pagination} pagination - The pagination parameters (page number and size).
 * @returns {Promise<ListResponse<ListItemForecastDto[]>>} A promise that resolves to the list of forecasts with pagination metadata.
 */
export const getForecasts = async (pagination: Pagination): Promise<ListResponse<ListItemForecastDto[]>> => {
  const params = urlParams(pagination);
  const response = await fetch(`${baseUrl}/weather/forecasts${params}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to fetch forecasts");
  }

  const result = await response.json();
  return {
    data: result.data,
    pagination: {
      totalRecords: result.pagination.totalRecords,
      totalPages: Math.ceil(result.pagination.totalRecords / pagination.pageSize),
      pageNumber: pagination.pageNumber,
      pageSize: pagination.pageSize,
    },
  };
};

/**
 * Fetches a single forecast by its ID.
 * 
 * @param {string} id - The unique ID of the forecast to retrieve.
 * @returns {Promise<ItemResponse<ItemForecastDto>>} A promise that resolves to the forecast details.
 */
export const getForecast = async (id: string): Promise<ItemResponse<ItemForecastDto>> => {
  const response = await fetch(`${baseUrl}/weather/forecasts/${id}`);
  if (!response.ok) {
    throw new Error("Failed to fetch forecast");
  }

  const result = await response.json();
  return result;
};

/**
 * Creates a new forecast using the provided data.
 * 
 * @param {Partial<ForecastDto>} data - Partial data of the warning to create a forecast.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to the creation response.
 */
export const createForecast = async (
  data: Partial<ForecastDto>
): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/weather/forecasts`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to create forecast");
  }

  const result = await response.json();
  return result;
};

/**
 * Initializes forecasts with a list of forecast data.
 * 
 * @param {ForecastDto[]} dataList - A list of forecast data to initialize.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to the initialization response.
 */
export const initializeForecasts = async (dataList: ForecastDto[]): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/weather/forecasts/initialize`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dataList),
  });

  if (!response.ok) {
    throw new Error("Failed to initialize forecasts");
  }

  const result = await response.json();
  return result;
};

/**
 * Updates an existing forecast by its ID with the provided data.
 * 
 * @param {string} id - The unique ID of the forecast to update.
 * @param {Partial<ForecastDto>} data - Partial data to update the forecast.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to the update response.
 */
export const updateForecast = async (
  id: string, data: Partial<ForecastDto>
): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/weather/forecasts/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to update forecast");
  }

  const result = await response.json();
  return result;
};

/**
 * Deletes a forecast by its ID.
 * 
 * @param {string} id - The unique ID of the forecast to delete.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to the deletion response.
 */
export const deleteForecast = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/weather/forecasts/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Failed to delete forecast");
  }

  const result = await response.json();
  return result;
};
