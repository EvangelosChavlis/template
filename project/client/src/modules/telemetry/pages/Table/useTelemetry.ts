// packages
import { useQuery } from 'react-query';
import { useState } from 'react';

// source
import { Pagination } from 'src/models/common/pagination';
import { getTelemetryList } from 'src/modules/telemetry/api/api';

const useTelemetry = () => {
  const [pagination, setPagination] = useState<Pagination>({
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0,
    totalPages: 0,
  });

  const { data: result } = useQuery(
    ['telemetry', pagination.pageNumber, pagination.pageSize],
    () => getTelemetryList(pagination),
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
    telemetryList: result?.data || [],
    pagination,
    handlePageChange
  };
};

export default useTelemetry;
