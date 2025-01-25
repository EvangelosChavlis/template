// packages
import Badge from "react-bootstrap/Badge";
import OverlayTrigger from "react-bootstrap/OverlayTrigger";
import Tooltip from "react-bootstrap/Tooltip";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Dropdown from "react-bootstrap/Dropdown";
import DropdownButton from "react-bootstrap/DropdownButton";

// source
import { TableFooterProps } from "src/models/shared/tableFooterProps";

const TableFooter = ({
  pagination,
  handlePageChange,
  rowsPerPage,
  onRowsPerPageChange,
}: TableFooterProps) => {
  const rowsPerPageOptions = [5, 10, 20, 50, 100];

  const { pageNumber, totalPages, totalRecords } = pagination;

  const isFirstPage = pageNumber === 1;
  const isLastPage = pageNumber === totalPages || totalRecords === 0;
  const displayedTotalPages = Math.max(totalPages, 1);

  return (
    <div className="d-flex align-items-center justify-content-between">
      <DropdownButton
        id="rows-per-page-dropdown"
        title={`Rows/Page: ${rowsPerPage}`}
        variant="light"
        onSelect={(eventKey) => {
          const selectedRows = parseInt(eventKey || "10", 10);
          onRowsPerPageChange(selectedRows);
        }}
      >
        {rowsPerPageOptions.map((option) => (
          <Dropdown.Item key={option} eventKey={option.toString()}>
            {option}
          </Dropdown.Item>
        ))}
      </DropdownButton>

      <Badge bg="light" className="mx-3 text-dark">
        Page: {pageNumber}/{displayedTotalPages}, Total Records: {totalRecords}
      </Badge>

      <ButtonGroup>
        <OverlayTrigger
          placement="top"
          overlay={<Tooltip>Go to the first page</Tooltip>}
        >
          <Button
            variant="light"
            disabled={isFirstPage}
            onClick={() => handlePageChange(1)}
          >
            <i className="bi bi-chevron-double-left" />
          </Button>
        </OverlayTrigger>

        <OverlayTrigger
          placement="top"
          overlay={<Tooltip>Go to the previous page</Tooltip>}
        >
          <Button
            variant="light"
            disabled={isFirstPage}
            onClick={() => handlePageChange(pageNumber - 1)}
          >
            <i className="bi bi-chevron-left" />
          </Button>
        </OverlayTrigger>

        <OverlayTrigger
          placement="top"
          overlay={<Tooltip>Go to the next page</Tooltip>}
        >
          <Button
            variant="light"
            disabled={isLastPage}
            onClick={() => handlePageChange(pageNumber + 1)}
          >
            <i className="bi bi-chevron-right" />
          </Button>
        </OverlayTrigger>

        <OverlayTrigger
          placement="top"
          overlay={<Tooltip>Go to the last page</Tooltip>}
        >
          <Button
            variant="light"
            disabled={isLastPage}
            onClick={() => handlePageChange(totalPages)}
          >
            <i className="bi bi-chevron-double-right" />
          </Button>
        </OverlayTrigger>
      </ButtonGroup>
    </div>
  );
};

export default TableFooter;