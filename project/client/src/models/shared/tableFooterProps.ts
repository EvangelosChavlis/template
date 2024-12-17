// source
import { Pagination } from "src/models/common/pagination";

export interface TableFooterProps {
    pagination: Pagination;
    handlePageChange: (pageNumber: number) => void;
}
  