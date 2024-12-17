// packages
import { useState } from "react";
import { useQuery } from "react-query";

// source
import { Pagination } from "src/models/common/pagination";
import { getUsers } from "src/modules/users/api/api";

const useUsers = () => {
  const initialPagination: Pagination = {
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  };

  const [pagination, setPagination] = useState<Pagination>(initialPagination);

  const { data: result, isLoading, isError, error } = useQuery(
    ['users', pagination.pageNumber, pagination.pageSize], // Query key includes pagination state
    () => getUsers(pagination),
    {
      keepPreviousData: true, // Keeps previous data while fetching new data
      onSuccess: (result) => {
        setPagination((prevPagination) => ({
          ...prevPagination,
          totalRecords: result.pagination.totalRecords,
          totalPages: result.pagination.totalPages,
        }));
      },
      onError: (err) => {
        console.error("Error fetching users:", err);
      },
    }
  );

  const handlePageChange = (pageNumber: number) => {
    setPagination((prev) => ({ ...prev, pageNumber }));
  };

  return {
    users: result?.data || [],
    pagination,
    handlePageChange,
    isLoading,
    isError,
    error,
  };
};

export default useUsers;
