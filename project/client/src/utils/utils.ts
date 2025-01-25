// source
import { Pagination } from "src/models/common/pagination";


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
export const baseUrl: string = import.meta.env.VITE_BASE_URL;

// URL Params Function
export const urlParams = (pagination: Pagination, filters: { filter: string, sortBy: string, sortOrder: "asc" | "desc" }) => {
  const { pageNumber, pageSize } = pagination;
  const { filter, sortBy, sortOrder } = filters;

  const params = new URLSearchParams();

  // Add pagination and filter
  if (filter) params.append('Filter', filter);
  params.append('PageNumber', String(pageNumber));
  params.append('PageSize', String(pageSize));

  // Add sorting parameters
  if (sortBy) params.append('SortBy', sortBy); // Column to sort by
  params.append('SortDescending', sortOrder === "desc" ? "true" : "false"); // Use true/false for sorting order

  return `?${params.toString()}`;
};


export const formatDateToISO = (inputDate: string): string => {
  if (inputDate === "") 
      return new Date().toISOString().split("T")[0];

  const [, datePart] = inputDate.split(" "); // Ignore the day name, focus on the date and time
  const [month, day, year] = datePart.split("/").map(Number); // Extract and parse the date components
  const parsedDate = new Date(year, month - 1, day); // Create a Date object
  return parsedDate.toISOString().split("T")[0]; // Return ISO date part
};

export const getAuthToken = () => {
  return localStorage.getItem("authToken");
};

// {
    //   title: "Initialize Users",
    //   action: () => navigate("initialize"),
    //   icon: <i className="bi bi-folder-plus"></i>,
    //   color: "success",
    //   placement: "top",
    //   disabled: false,
    // },