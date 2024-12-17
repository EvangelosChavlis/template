// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Badge from "react-bootstrap/Badge";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import { ListItemTelemetryDto } from "src/models/metrics/telemetryDto";
import { incNumberFunction } from "src/utils/utils";
import TableFooter from "src/modules/shared/TableFooter";
import Header from "src/modules/shared/Header";
import useTelemetry from "src/modules/telemetry/pages/Table/useTelemetry";
import LoadingSpinner from "src/modules/shared/LoadingSpinner";

const Telemetry = () => {
  const navigate = useNavigate();

  const { 
    telemetryList, 
    pagination, 
    handlePageChange 
  } = useTelemetry();

  const navigateClick = (telemetryId: string) => {
    navigate(`/telemetry/${telemetryId}`);
  };

  if (!telemetryList) return <LoadingSpinner />;

  const header = "Telemetry Page";
  const buttons: ButtonProps[] = [];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        {telemetryList.length > 0 ? (
          <Table 
            responsive 
            hover 
            className="shadow-sm"
            style={{
              borderCollapse: "collapse",
            }}
          >
            <thead className="bg-primary text-white">
              <tr>
                <th className="text-center" style={{ width: "5%" }}>#</th>
                <th className="text-center" style={{ width: "10%" }}>
                  <i className="bi bi-arrow-right-circle-fill icon-margin-right" />
                  <span>Method</span>
                </th>
                <th className="text-center" style={{ width: "25%" }}>
                  <i className="bi bi-file-earmark-code-fill icon-margin-right" />
                  <span>Path</span>
                </th>
                <th className="text-center" style={{ width: "15%" }}>
                  <i className="bi bi-patch-check-fill icon-margin-right" />
                  <span>Status Code</span>
                </th>
                <th className="text-center" style={{ width: "20%" }}>
                  <i className="bi bi-speedometer2 icon-margin-right" />
                  <span>Response Time (ms)</span>
                </th>
                <th className="text-center" style={{ width: "25%" }}>
                  <i className="bi bi-clock-history icon-margin-right" />
                  <span>Timestamp</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {telemetryList.map((telemetry: ListItemTelemetryDto, idx) => (
                <tr key={telemetry.id}>
                  <td className="text-center">
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center">
                  <Button
                      variant="link"
                      className={`text-decoration-none text-primary fw-bold`}
                      onClick={() => navigateClick(telemetry.id)}
                    >
                      {telemetry.method}
                    </Button>
                  </td>
                  <td className="text-center">
                    {telemetry.path}
                  </td>
                  <td className="text-center">
                    <Badge bg="dark" pill>
                      {telemetry.statusCode}
                    </Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg="info">
                      {telemetry.responseTime} ms
                    </Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg="primary">
                      {telemetry.requestTimestamp}
                    </Badge>  
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          <div className="alert alert-warning text-center">
            No telemetry data found.
          </div>
        )}
        <TableFooter pagination={pagination} handlePageChange={handlePageChange} />
      </div>
    </div>
  );
};

export default Telemetry;
