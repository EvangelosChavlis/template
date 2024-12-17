// source
import { Pagination } from "../models/common/pagination";

// Increment Number Function
export const incNumberFunction = (idx: number, pagination: Pagination): number => {
return idx + 1 + (pagination.pageNumber - 1) * pagination.pageSize;
};

export const getBadgeColor = (warning: string) => {
    switch (warning) {
      case "Extreme":
        return "danger";
      case "High":
        return "warning";
      case "Normal":
        return "info";
      case "Low":
        return "success";
      default:
        return "secondary";
    }
  };

// Base URLs
export const baseUrl: string = `http://localhost:5000/api`;

// URL Params Function
export const urlParams = (pagination: Pagination): string => {
  return `?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}`;
};

export const formatDateToISO = (inputDate: string): string => {
  if (inputDate === "") 
      return new Date().toISOString().split("T")[0];

  const [, datePart] = inputDate.split(" "); // Ignore the day name, focus on the date and time
  const [month, day, year] = datePart.split("/").map(Number); // Extract and parse the date components
  const parsedDate = new Date(year, month - 1, day); // Create a Date object
  return parsedDate.toISOString().split("T")[0]; // Return ISO date part
};

