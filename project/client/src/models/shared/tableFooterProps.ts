// source
import { Pagination } from "src/models/common/pagination";

export interface TableFooterProps {
    rowsPerPage: number;
    onRowsPerPageChange: (rows: number) => void;
    pagination: Pagination;
    handlePageChange: (pageNumber: number) => void;
}
  