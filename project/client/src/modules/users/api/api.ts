// source
import { ItemUserDto, ListItemUserDto, UserDto } from "src/models/auth/usersDto";
import { CommandResponse } from "src/models/common/commandResponse";
import { ItemResponse } from "src/models/common/itemResponse";
import { ListResponse } from "src/models/common/listResponse";
import { Pagination } from "src/models/common/pagination";
import { baseUrl, urlParams } from "src/utils/utils";

export const getUsers = async (pagination: Pagination): Promise<ListResponse<ListItemUserDto[]>> => {
  const params = urlParams(pagination);
  const response = await fetch(`${baseUrl}/auth/users${params}`, {
    method: "GET",
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

export const getUser = async (id: string): Promise<ItemResponse<ItemUserDto>> => {
  const response = await fetch(`${baseUrl}/auth/users/${id}`);
  if (!response.ok) {
    throw new Error("Failed to fetch user");
  }

  const result = await response.json();
  return result;
};

export const createUser = async (
  data: Partial<UserDto>
): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/create`, {
    method: "POST",
    headers: {
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
    throw new Error("Failed to create user");
  }

  const result = await response.json();
  return result;
};

export const activateUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/activate/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to activate user");
  }

  const result = await response.json();
  return result.data;
};

export const deactivateUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/deactivate/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to deactivate user");
  }

  const result = await response.json();
  return result.data;
};

export const lockUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/lock/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to lock user");
  }

  const result = await response.json();
  return result.data;
};


export const unlockUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/unlock/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to unlock user");
  }

  const result = await response.json();
  return result.data;
};

export const generatePasswordUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/generate-password/${id}`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to generate password for user");
  }

  const result = await response.json();
  return result.data;
};

export const deleteUser = async (id: string): Promise<CommandResponse<string>> => {
  const response = await fetch(`${baseUrl}/auth/users/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Failed to delete user");
  }

  const result = await response.json();
  return result.data;
};
