// Source
import { ItemUserDto, ListItemUserDto, UserDto } from "src/models/auth/usersDto";
import { CommandResponse } from "src/models/common/commandResponse";
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { baseUrl, getAuthToken, urlParams } from "src/utils/utils";

/**
 * Fetches a paginated list of users with sorting and filtering.
 * @param {Pagination} pagination - Pagination details (page number, page size).
 * @param filter - Filter term for searching users.
 * @param sortBy - The column to sort by (e.g., 'FirstName').
 * @param sortOrder - The order of sorting, either 'asc' or 'desc'.
 * @returns {Promise<ListResponse<ListItemUserDto[]>>}  A list of users with pagination metadata.
 */
export const getUsers = async (
  pagination: Pagination,
  filter: string = '',
  sortBy: string = 'FirstName',
  sortOrder: "asc" | "desc" = "asc"
): Promise<ListResponse<ListItemUserDto[]>> => {
  const token = getAuthToken();
  const params = urlParams(pagination, { filter, sortBy, sortOrder }); 
  const response = await fetch(`${baseUrl}/auth/users${params}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to fetch users");
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
 * Fetches details of a single user by ID.
 * @param id - User ID.
 * @returns User details.
 */
export const getUser = async (id: string): Promise<ItemResponse<ItemUserDto>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/users/${id}`,
    {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,
      },
    }
  );
  if (!response.ok) {
    throw new Error("Failed to fetch user");
  }

  const result = await response.json();
  return result;
};

/**
 * Creates a new user.
 * @param data - Partial user details.
 * @returns A response containing the ID of the newly created user.
 */
export const createUser = async (
  data: Partial<UserDto>
): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/users/create`, {
    method: "POST",
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to create user");
  }

  const result = await response.json();
  return result;
};

/**
 * Updates an existing user by ID.
 * @param id - User ID.
 * @param data - Partial user details to update.
 * @returns A response confirming the update.
 */
export const updateUser = async (
  id: string,
  data: Partial<UserDto>
): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/update/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error("Failed to update user");
  }

  const result = await response.json();
  return result;
};

/**
 * Activates a user by ID.
 * @param id - User ID to activate.
 * @returns A response confirming the activation.
 */
export const activateUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/activate/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to activate user");
  }

  const result = await response.json();
  return result;
};

/**
 * Deactivates a user by ID.
 * @param id - User ID to deactivate.
 * @returns A response confirming the deactivation.
 */
export const deactivateUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/deactivate/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to deactivate user");
  }

  const result = await response.json();
  return result;
};

/**
 * Locks a user account by ID.
 * @param id - User ID to lock.
 * @returns A response confirming the account lock.
 */
export const lockUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/lock/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to lock user");
  }

  const result = await response.json();
  return result;
};

/**
 * Unlocks a user account by ID.
 * @param id - User ID to unlock.
 * @returns A response confirming the account unlock.
 */
export const unlockUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/unlock/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to unlock user");
  }

  const result = await response.json();
  return result;
};

/**
 * Generates a new password for a user by ID.
 * @param id - User ID to generate a password for.
 * @returns A response containing the new password or confirmation.
 */
export const generatePasswordUser = async (id: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/users/generate-password/${id}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to generate password for user");
  }

  const result = await response.json();
  return result;
};

/**
 * Assigns a role to a user.
 * @param userId - ID of the user.
 * @param roleId - ID of the role to assign.
 * @returns A response confirming the role assignment.
 */
export const assingRoleToUser = async (userId: string, roleId: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/users/assign/${userId}/${roleId}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to assign role to user");
  }

  const result = await response.json();
  return result;
};

/**
 * Unassigns a role from a user.
 * @param userId - ID of the user.
 * @param roleId - ID of the role to unassign.
 * @returns A response confirming the role removal.
 */
export const unassingRoleFromUser = async (userId: string, roleId: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/users/unassign/${userId}/${roleId}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to unassign role from user");
  }

  const result = await response.json();
  return result;
};

/**
 * Deletes a user by ID.
 * @param id - User ID to delete.
 * @returns A response confirming the deletion.
 */
export const deleteUser = async (id: string): Promise<CommandResponse<string>> => {
  const token = getAuthToken();
  const response = await fetch(`${baseUrl}/auth/users/${id}`, {
    method: "DELETE",
    headers: {
      "Authorization": `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to delete user");
  }

  const result = await response.json();
  return result;
};
