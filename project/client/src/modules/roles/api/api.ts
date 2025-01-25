import { ItemRoleDto, RoleDto } from "src/models/auth/roleDto";
import { CommandResponse } from "src/models/common/commandResponse";
import { ItemResponse } from "src/models/common/itemResponse";
import { baseUrl, getAuthToken } from "src/utils/utils";

/**
 * Fetches a list of all roles.
 * 
 * @returns {Promise<ItemRoleDto[]>} A promise that resolves to an array of role items.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getRoles = async (): Promise<ItemRoleDto[]> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to fetch roles");
  }

  const result = await response.json();
  return result;
};

/**
 * Fetches a specific role by its ID.
 * 
 * @param {string} id - The ID of the role to fetch.
 * @returns {Promise<ItemResponse<ItemRoleDto>>} A promise that resolves to the role data.
 * @throws {Error} Throws an error if the fetch request fails.
 */
export const getRole = async (id: string): Promise<ItemResponse<ItemRoleDto>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to fetch role");
  }

  const result = await response.json();
  return result;
};

/**
 * Activates a role by its ID.
 * 
 * @param {string} id - The ID of the role to activate.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the activation request fails.
 */
export const activateRole = async (id: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles/activate/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to activate role");
  }

  const result = await response.json();
  return result;
};

/**
 * Deactivates a role by its ID.
 * 
 * @param {string} id - The ID of the role to deactivate.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the deactivation request fails.
 */
export const deactivateRole = async (id: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles/deactivate/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to deactivate role");
  }

  const result = await response.json();
  return result;
};

/**
 * Creates a new role.
 * 
 * @param {Partial<RoleDto>} data - The data to create the new role.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the creation request fails.
 */
export const createRole = async (
  data: Partial<RoleDto>
): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to create role");
  }

  const result = await response.json();
  return result;
};

/**
 * Initializes roles with the given data list.
 * 
 * @param {RoleDto[]} dataList - The list of roles to initialize.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to the initialization response.
 * @throws {Error} Throws an error if the initialization request fails. 
 */
export const initializeRoles = async (dataList: RoleDto[]): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles/initialize`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify(dataList),
  });

  if (!response.ok) {
    throw new Error("Failed to initialize roles");
  }

  const result = await response.json();
  return result;
};

/**
 * Updates an existing role.
 * 
 * @param {string} id - The ID of the role to update.
 * @param {Partial<RoleDto>} data - The updated role data.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the update request fails. 
 */
export const updateRole = async (
  id: string, data: Partial<RoleDto>
): Promise<CommandResponse<string>> => {
  const token = getAuthToken(); // Retrieve token from localStorage
  const response = await fetch(`${baseUrl}/auth/roles/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`, // Add Bearer token in header
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to update role");
  }

  const result = await response.json();
  return result;
};

/**
 * Deletes a role by its ID.
 * 
 * @param {string} id - The ID of the role to delete.
 * @returns {Promise<CommandResponse<string>>} A promise that resolves to a response indicating success or failure.
 * @throws {Error} Throws an error if the delete request fails.
 */
export const deleteRole = async (id: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/roles/${id}`, {
    method: "DELETE",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to delete role");
  }

  const result = await response.json();
  return result;
};
