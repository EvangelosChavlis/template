// source
import { baseUrl, getAuthToken } from "src/utils/utils";

export const clearData = async () => {
    const token = getAuthToken();
    const response = await fetch(`${baseUrl}/data/clear`, {
        method: "GET",
        headers: {
          "Authorization": `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error("Failed to clear data");
    }

    const result = await response.json();
    return result;
};
  
export const seedData = async () => {
    const token = getAuthToken();
    const response = await fetch(`${baseUrl}/data/seed`, {
        method: "GET",
        headers: {
          "Authorization": `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error("Failed to seed data");
    }

    const result = await response.json();
    return result;
};