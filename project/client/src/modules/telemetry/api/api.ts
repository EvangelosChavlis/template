// source
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { ItemTelemetryDto, ListItemTelemetryDto } from "src/models/metrics/telemetryDto";
import { baseUrl, getAuthToken, urlParams } from "src/utils/utils";

/**
 * Fetches a list of telemetry items with pagination, filtering, and sorting.
 * 
 * @param {Pagination} pagination - Pagination details (page number, page size).
 * @param {string} [filter=''] - Optional filter term for searching telemetry items.
 * @param {string} [sortBy='Method'] - Optional column to sort by (e.g., 'Method').
 * @param {"asc" | "desc"} [sortOrder='asc'] - Optional sort order, either 'asc' or 'desc'.
 * @returns {Promise<ListResponse<ListItemTelemetryDto[]>>} A promise resolving to the telemetry list with pagination info.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getTelemetryList = async (
  pagination: Pagination,
  filter: string = '',
  sortBy: string = 'Method',
  sortOrder: "asc" | "desc" = "asc"
): Promise<ListResponse<ListItemTelemetryDto[]>> => {
  const token = getAuthToken();
  const params = urlParams(pagination, { filter, sortBy, sortOrder });
  const response = await fetch(`${baseUrl}/metrics/telemetry${params}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to fetch telemetry list");
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
 * Fetches a specific telemetry item by its ID.
 * 
 * @param {string} id - The ID of the telemetry item to fetch.
 * @returns {Promise<ItemResponse<ItemTelemetryDto>>} A promise resolving to the telemetry item data.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getTelemetryItem = async (id: string): Promise<ItemResponse<ItemTelemetryDto>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/metrics/telemetry/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });
  
  if (!response.ok) {
    throw new Error("Failed to fetch telemetry item");
  }

  const result = await response.json();
  return result;
};
