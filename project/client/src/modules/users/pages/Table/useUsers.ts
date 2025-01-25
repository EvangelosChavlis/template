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

  const [filter, setFilter] = useState<string>(''); 
  const [sortBy, setSortBy] = useState<string>('');
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

  const [pagination, setPagination] = useState<Pagination>(initialPagination);

  const { data: result, isLoading, isError, error } = useQuery(
    ['users', pagination.pageNumber, pagination.pageSize, filter, sortBy, sortOrder],
    () => getUsers(pagination, filter, sortBy, sortOrder),
    {
      keepPreviousData: true,
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

  const handleRowsPerPageChange = (pageSize: number) => {
    setPagination((prev) => ({ ...prev, pageSize, pageNumber: 1 }));
  };

  return {
    users: result?.data || [],
    pagination,
    handlePageChange,
    handleRowsPerPageChange,
    handleFilterChange,
    handleSortByChange,
    isLoading,
    isError,
    error,
    sortBy,
    sortOrder,
    filter
  };
};

export default useUsers;
