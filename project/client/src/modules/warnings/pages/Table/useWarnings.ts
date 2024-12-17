// packages
import { useQuery } from 'react-query';
import { useState } from 'react';

// source
import { getWarnings } from 'src/modules/warnings/api/api';
import { Pagination } from 'src/models/common/pagination';

const useWarnings = () => {
  const [pagination, setPagination] = useState<Pagination>({
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  });

  const { data, isLoading, isError, error } = useQuery(
    ['warnings', pagination.pageNumber, pagination.pageSize],
    () => getWarnings(pagination),
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
    setPagination((prevPagination) => ({ ...prevPagination, pageNumber }));
  };

  return {
    warnings: data?.data || [],
    pagination,
    handlePageChange,
    isLoading,
    isError,
    error,
  };
};

export default useWarnings;
