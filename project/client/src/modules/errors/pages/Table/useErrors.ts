// packages
import { useQuery } from 'react-query';
import { useState } from 'react';

// source
import { getErrors } from 'src/modules/errors/api/api';
import { Pagination } from 'src/models/common/pagination';

const useErrors = () => {
  const [pagination, setPagination] = useState<Pagination>({
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  });

  const { data: result, isLoading, isError, error } = useQuery(
    ['errors', pagination.pageNumber, pagination.pageSize],
    () => getErrors(pagination),
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
    errors: result?.data || [],
    pagination,
    handlePageChange,
    isLoading,
    isError,
    error,
  };
};

export default useErrors;
