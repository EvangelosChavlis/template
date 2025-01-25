// source
import { CommandResponse } from "src/models/common/commandResponse";
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { 
  ListItemWarningDto, 
  PickerWarningDto, 
  ItemWarningDto, 
  WarningDto 
} from "src/models/weather/warningsDto";
import { urlParams, baseUrl, getAuthToken } from "src/utils/utils";

/**
 * Fetches a list of warnings with pagination, filtering, and sorting.
 * 
 * @param {Pagination} pagination - Pagination information (e.g., page number, page size).
 * @param {string} [filter=''] - The filter term for searching warnings.
 * @param {string} [sortBy='Name'] - The column to sort by (e.g., 'Name').
 * @param {"asc" | "desc"} [sortOrder='asc'] - The order of sorting, either 'asc' or 'desc'.
 * @returns {Promise<ListResponse<ListItemWarningDto[]>>} A promise that resolves to a list of warning items with pagination info.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getWarnings = async (
  pagination: Pagination,
  filter: string = '',
  sortBy: string = 'Name',
  sortOrder: "asc" | "desc" = "asc"
): Promise<ListResponse<ListItemWarningDto[]>> => {
  const token = getAuthToken();
  const params = urlParams(pagination, { filter, sortBy, sortOrder });
  const response = await fetch(`${baseUrl}/weather/warnings${params}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to fetch warnings");
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
 * Fetches a list of warnings for use in a picker (e.g., dropdown or selection component).
 * 
 * @returns {Promise<ItemResponse<PickerWarningDto[]>>} A promise that resolves to a list of picker warnings.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getWarningsPicker = async (): Promise<ItemResponse<PickerWarningDto[]>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/weather/warnings/picker`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });
  
  if (!response.ok) {
    throw new Error("Failed to fetch warnings picker");
  }

  const result = await response.json();
  return result;
};

/**
 * Fetches a specific warning by its ID.
 * 
 * @param {string} id - The ID of the warning to fetch.
 * @returns {Promise<ItemResponse<ItemWarningDto>>} A promise that resolves to the warning data.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getWarning = async (id: string): Promise<ItemResponse<ItemWarningDto>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/weather/warnings/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });
  
  if (!response.ok) {
    throw new Error("Failed to fetch warning");
  }

  const result = await response.json();
  return result;
};

/**
 * Creates a new warning.
 * 
 * @param {Partial<WarningDto>} data - The data to create the new warning.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the creation request fails.
 */
export const createWarning = async (
  data: Partial<WarningDto>
): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/weather/warnings`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to create warning");
  }

  const result = await response.json();
  return result;
};

/**
 * Initializes warning with the given data list.
 * 
 * @param {WarningDto[]} dataList - The list of roles to initialize.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to the initialization response.
 */
export const initializeWarnings = async (dataList: WarningDto[]): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/weather/warnings/initialize`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify(dataList),
  });


  if (!response.ok) {
    throw new Error("Failed to initialize warning");
  }

  const result = await response.json();
  return result;
};

/**
 * Updates an existing warning.
 * 
 * @param {string} id - The ID of the warning to update.
 * @param {Partial<WarningDto>} data - The updated warning data.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the update request fails.
 */
export const updateWarning = async (
  id: string, data: Partial<WarningDto>
): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/weather/warnings/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to update warning");
  }

  const result = await response.json();
  return result;
};

/**
 * Deletes a warning by its ID.
 * 
 * @param {string} id - The ID of the warning to delete.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the delete request fails.
 */
export const deleteWarning = async (id: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/weather/warnings/${id}`, {
    method: "DELETE",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to delete warning");
  }

  const result = await response.json();
  return result;
};
