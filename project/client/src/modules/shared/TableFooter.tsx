// packages
import Button from "react-bootstrap/Button";

// source
import { TableFooterProps } from "src/models/shared/tableFooterProps";

const TableFooter = ({ pagination, handlePageChange }: TableFooterProps) => {
  return (
      <div className="d-flex align-items-center justify-content-center">
        <Button
          variant="primary"
          disabled={pagination.pageNumber === 1}
          onClick={() => handlePageChange(pagination.pageNumber - 1)}
        >
          <i className="bi bi-caret-left-fill"></i>
        </Button>
        <span className="mx-3">
          Page: {pagination.pageNumber}/{pagination.totalPages}, Total Records: {pagination.totalRecords}
        </span>
        <Button
          variant="primary"
          disabled={pagination.pageNumber === pagination.totalPages || pagination.totalRecords === 0}
          onClick={() => handlePageChange(pagination.pageNumber + 1)}
        >
          <i className="bi bi-caret-right-fill"></i>
        </Button>
      </div>
  );
};

export default TableFooter;
