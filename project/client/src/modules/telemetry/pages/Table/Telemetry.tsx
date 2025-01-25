// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Badge from "react-bootstrap/Badge";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import { incNumberFunction } from "src/utils/utils";
import TableFooter from "src/modules/shared/TableFooter";
import Header from "src/modules/shared/Header";
import SortIcon from "src/modules/shared/SortIcon";
import useTelemetry from "src/modules/telemetry/pages/Table/useTelemetry";

const Telemetry = () => {
  const navigate = useNavigate();

  const {
    telemetryList,
    pagination,
    handlePageChange,
    handleRowsPerPageChange,
    handleFilterChange,
    handleSortByChange,
    sortBy,
    sortOrder,
    filter,
  } = useTelemetry();

  const navigateClick = (telemetryId: string) => {
    navigate(`/telemetry/${telemetryId}`);
  };

  const header = "Telemetry Page";
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
            placeholder="Search telemetry..."
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
                  style={{ width: "10%", cursor: "pointer" }}
                  onClick={() => handleSortByChange("Method")}
                >
                  <SortIcon
                    column="Method"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-arrow-right-circle-fill icon-margin-right" />
                  <span>Method</span>
                </th>
                <th
                  className="text-center"
                  style={{ width: "25%", cursor: "pointer" }}
                  onClick={() => handleSortByChange("Path")}
                >
                  <SortIcon
                    column="Path"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-file-earmark-code-fill icon-margin-right" />
                  <span>Path</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "15%" }}
                  onClick={() => handleSortByChange("StatusCode")}
                >
                  <SortIcon
                    column="StatusCode"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-patch-check-fill icon-margin-right" />
                  <span>Status Code</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "20%" }}
                  onClick={() => handleSortByChange("ResponseTime")}
                >
                  <SortIcon
                    column="ResponseTime"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-speedometer2 icon-margin-right" />
                  <span>Response Time (ms)</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "25%" }}
                  onClick={() => handleSortByChange("RequestTimestamp")}
                >
                  <SortIcon
                    column="RequestTimestamp"
                    sortBy={sortBy}
                    sortOrder={sortOrder}
                  />
                  <i className="bi bi-clock-history icon-margin-right" />
                  <span>Timestamp</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {telemetryList.map((telemetry, idx) => (
                <tr key={telemetry.id}>
                  <td className="text-center">
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center">
                    <Button
                      variant="link"
                      className="text-decoration-none text-primary fw-bold"
                      onClick={() => navigateClick(telemetry.id)}
                    >
                      {telemetry.method}
                    </Button>
                  </td>
                  <td className="text-center">{telemetry.path}</td>
                  <td className="text-center">
                    <Badge bg="dark" pill>
                      {telemetry.statusCode}
                    </Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg="info">{telemetry.responseTime} ms</Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg="primary">{telemetry.requestTimestamp}</Badge>
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

export default Telemetry;
