// packages
import { useQuery } from 'react-query';
import { useState } from 'react';

// source
import { Pagination } from 'src/models/common/pagination';
import { getTelemetryList } from 'src/modules/telemetry/api/api';

const useTelemetry = () => {
  const initialPagination: Pagination = {
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  };

  const [pagination, setPagination] = useState<Pagination>(initialPagination);
  const [filter, setFilter] = useState<string>('');
  const [sortBy, setSortBy] = useState<string>('Method');
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

  const { data: telemetry, isLoading, isError, error } = useQuery(
    ['telemetry', pagination.pageNumber, pagination.pageSize, filter, sortBy, sortOrder],
    () => getTelemetryList(pagination, filter, sortBy, sortOrder),
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
        console.error("Error fetching telemetry:", err);
      },
    }
  );

  const handlePageChange = (pageNumber: number) => {
    setPagination((prevPagination) => ({ ...prevPagination, pageNumber }));
  };

  const handleRowsPerPageChange = (pageSize: number) => {
    setPagination((prevPagination) => ({ ...prevPagination, pageSize, pageNumber: 1 }));
  };

  return {
    telemetryList: telemetry?.data || [],
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

export default useTelemetry;
