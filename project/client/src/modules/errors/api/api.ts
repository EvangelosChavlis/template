// source
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { ItemErrorDto, ListItemErrorDto } from "src/models/metrics/errorsDto";
import { baseUrl, getAuthToken, urlParams } from "src/utils/utils";

/**
 * Fetches a list of errors with pagination, filtering, and sorting.
 * 
 * @param {Pagination} pagination - Pagination information (e.g., page number, page size).
 * @param {string} [filter=''] - The filter term for searching errors.
 * @param {string} [sortBy='Error'] - The column to sort by (e.g., 'Error').
 * @param {"asc" | "desc"} [sortOrder='asc'] - The order of sorting, either 'asc' or 'desc'.
 * @returns {Promise<ListResponse<ListItemErrorDto[]>>} A promise that resolves to a list of error items with pagination info.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getErrors = async (
    pagination: Pagination,
    filter: string = '',
    sortBy: string = 'Error',
    sortOrder: "asc" | "desc" = "asc"
  ): Promise<ListResponse<ListItemErrorDto[]>> => {
    const token = getAuthToken();
    const params = urlParams(pagination, { filter, sortBy, sortOrder });
    const response = await fetch(`${baseUrl}/metrics/errors${params}`, {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,
      },
    });
  
    if (!response.ok) {
      throw new Error("Failed to fetch errors");
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
 * Fetches a specific error by its ID.
 * 
 * @param {string} id - The ID of the error to fetch.
 * @returns {Promise<ItemResponse<ItemErrorDto>>} A promise that resolves to the error data.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getError = async (id: string): Promise<ItemResponse<ItemErrorDto>> => {
  const token = getAuthToken();  
  const response = await fetch(`${baseUrl}/metrics/errors/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });
  
  if (!response.ok) {
      throw new Error("Failed to fetch error");
  }

  const result = await response.json();
  return result;
};