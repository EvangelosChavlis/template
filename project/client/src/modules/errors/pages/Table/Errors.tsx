// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Badge from "react-bootstrap/Badge";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import { ListItemErrorDto } from "src/models/metrics/errorsDto";
import { incNumberFunction } from "src/utils/utils";
import TableFooter from "src/modules/shared/TableFooter";
import Header from "src/modules/shared/Header";
import SortIcon from "src/modules/shared/SortIcon";
import useErrors from "src/modules/errors/pages/Table/useErrors";

const Errors = () => {
  const navigate = useNavigate();

  const {
    errors,
    pagination,
    handlePageChange,
    handleRowsPerPageChange,
    handleFilterChange,
    handleSortByChange,
    sortBy,
    sortOrder,
    filter,
  } = useErrors();

  const navigateClick = (errorId: string) => {
    navigate(`/errors/${errorId}`);
  };

  const header = "Errors Page";
  const buttons: ButtonProps[] = [];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        <div className="mb-3">
          <input
            type="text"
            value={filter}
            onChange={handleFilterChange}
            placeholder="Search errors..."
            className="form-control"
          />
        </div>

        <div style={{ maxHeight: "60vh", overflowY: "auto" }}>
          <Table
            responsive
            hover
            className="shadow-sm"
            style={{ borderCollapse: "collapse" }}
          >
            <thead className="bg-primary text-white">
              <tr>
                <th className="text-center" style={{ width: "5%" }}>#</th>
                <th
                  className="text-center"
                  style={{ width: "40%", cursor: "pointer" }}
                  onClick={() => handleSortByChange("Error")}
                >
                  <SortIcon
                    column="Error"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-exclamation-circle icon-margin-right" />
                  <span>Error</span>
                </th>
                <th
                  className="text-center"
                  style={{ width: "15%", cursor: "pointer" }}
                  onClick={() => handleSortByChange("StatusCode")}
                >
                  <SortIcon
                    column="StatusCode"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-info-circle icon-margin-right" />
                  <span>Status Code</span>
                </th>
                <th
                  className="text-center"
                  style={{ width: "40%", cursor: "pointer" }}
                  onClick={() => handleSortByChange("Timestamp")}
                >
                  <SortIcon
                    column="Timestamp"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-calendar-check icon-margin-right" />
                  <span>Timestamp</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {errors.map((error: ListItemErrorDto, idx) => (
                <tr key={error.id}>
                  <td className="text-center">
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center">
                    <Button
                      variant="link"
                      className="text-decoration-none text-primary fw-bold"
                      onClick={() => navigateClick(error.id)}
                    >
                      {error.error}
                    </Button>
                  </td>
                  <td className="text-center">
                    <Badge bg="dark" pill>
                      {error.statusCode}
                    </Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg="primary">
                      {error.timestamp}
                    </Badge>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </div>

        <TableFooter
          pagination={pagination}
          handlePageChange={handlePageChange}
          rowsPerPage={pagination.pageSize}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </div>
    </div>
  );
};

export default Errors;
