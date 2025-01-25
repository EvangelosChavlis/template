// packages
import { useState } from "react";
import { useQuery } from "react-query";

// source
import { Pagination } from "src/models/common/pagination";
import { getForecasts } from "src/modules/forecasts/api/api";

const useForecasts = () => {
  const initialPagination: Pagination = {
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  };

  const [pagination, setPagination] = useState<Pagination>(initialPagination);
  const [filter, setFilter] = useState<string>('');
  const [sortBy, setSortBy] = useState<string>('Date');
  const [sortOrder, setSortOrder] = useState<"asc" | "desc">("asc");

  const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFilter(e.target.value);
  };

  const handleSortByChange = (value: string | null) => {
    if (value) {
      if (value === sortBy) {
        setSortOrder((prev) => (prev === "asc" ? "desc" : "asc"));
      } else {
        setSortBy(value);
        setSortOrder("asc");
      }
    }
  };

  const { data: results, isLoading, isError, error } = useQuery(
    ["forecasts", pagination.pageNumber, pagination.pageSize, filter, sortBy, sortOrder],
    () => getForecasts(pagination, filter, sortBy, sortOrder),
    {
      keepPreviousData: true,
      onSuccess: (result) => {
        setPagination((prev) => ({
          ...prev,
          totalRecords: result.pagination.totalRecords,
          totalPages: result.pagination.totalPages,
        }));
      },
      onError: (err) => {
        console.error("Error fetching forecasts:", err);
      },
    }
  );

  // Handle page change
  const handlePageChange = (pageNumber: number) => {
    setPagination((prev) => ({ ...prev, pageNumber }));
  };

  // Handle rows per page change
  const handleRowsPerPageChange = (pageSize: number) => {
    setPagination((prev) => ({ ...prev, pageSize, pageNumber: 1 }));
  };

  return {
    forecasts: results?.data || [],
    pagination,
    handlePageChange,
    handleRowsPerPageChange,
    handleFilterChange,
    handleSortByChange,
    isLoading,
    isError,
    error,
    filter,
    sortBy,
    sortOrder,
  };
};

export default useForecasts;
