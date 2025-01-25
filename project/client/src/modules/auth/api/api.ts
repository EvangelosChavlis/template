import { AuthenticatedUserDto, UserDto, UserLoginDto } from "src/models/auth/usersDto";
import { CommandResponse } from "src/models/common/commandResponse";
import { baseUrl } from "src/utils/utils";

/**
 * Logs in a user with the provided credentials.
 * 
 * @param {UserLoginDto} credentials - The username and password of the user.
 * @returns {Promise<CommandResponse<AuthenticatedUserDto>>} A promise resolving to the login response.
 * @throws {Error} Throws an error if the login request fails.
 */
export const login = async (
    credentials: UserLoginDto
): Promise<CommandResponse<AuthenticatedUserDto>> => {
    const response = await fetch(`${baseUrl}/auth/users/login`, {
        method: "POST",
        headers: {
        "Content-Type": "application/json",
        },
        body: JSON.stringify(credentials),
    });

    if (!response.ok) {
        const errorDetails = await response.json();
        throw new Error(errorDetails?.message || "Failed to login");
    }

    const result: CommandResponse<AuthenticatedUserDto> = await response.json();
    return result;
};

/**
 * Registers a new user.
 * @param data - Partial user details.
 * @returns A response containing the ID of the newly created user.
 */
export const register = async (
    data: Partial<UserDto> 
): Promise<CommandResponse<string>> => {
    const response = await fetch(`${baseUrl}/auth/users/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });

    if (!response.ok) {
        throw new Error("Failed to register user");
    }

    const result = await response.json();
    return result;
};