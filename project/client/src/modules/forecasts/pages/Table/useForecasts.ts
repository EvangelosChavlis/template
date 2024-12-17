// packages
import { useQuery } from "react-query";
import { useState } from "react";

// source
import { Pagination } from "src/models/common/pagination";
import { getForecasts } from "src/modules/forecasts/api/api";

const useForecasts = () => {

  // Local state for pagination
  const [pagination, setPagination] = useState<Pagination>({
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  });

  // Fetch forecasts using React Query
  const { data: results, isLoading, isError, error } = useQuery(
    ["forecasts", pagination.pageNumber, pagination.pageSize],
    () => getForecasts(pagination),
    {
      keepPreviousData: true, 
      onSuccess: (result) => {
        setPagination((prev) => ({
          ...prev,
          totalRecords: result.pagination.totalRecords,
          totalPages: result.pagination.totalPages,
        }));
      },
    }
  );

  const handlePageChange = (pageNumber: number) => {
    setPagination((prev) => ({ ...prev, pageNumber }));
  };

  return {
    forecasts: results?.data || [],
    pagination,
    handlePageChange,
    isLoading,
    isError,
    error,
  };
};

export default useForecasts;
