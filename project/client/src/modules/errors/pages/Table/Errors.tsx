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
import useErrors from "src/modules/errors/pages/Table/useErrors";
import LoadingSpinner from "src/modules/shared/LoadingSpinner";

const Errors = () => {
  const navigate = useNavigate();

  const { 
    errors, 
    pagination, 
    handlePageChange
  } = useErrors();

  const navigateClick = (errorId: string) => {
    navigate(`/errors/${errorId}`);
  };

  if (!errors) return <LoadingSpinner />;

  const header = "Errors Page";
  const buttons: ButtonProps[] = [];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        {errors.length > 0 ? (
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
                <th className="text-center" style={{ width: "40%" }}>
                  <i className="bi bi-clock-history icon-margin-right" />
                  <span>Error</span>
                </th>
                <th className="text-center" style={{ width: "15%" }}>
                  <i className="bi bi-info-circle icon-margin-right" />
                  <span>Status Code</span>
                </th>
                <th className="text-center" style={{ width: "40%" }}>
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
                      className={`text-decoration-none text-primary fw-bold`}
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
        ) : (
          <div className="alert alert-warning text-center">
            No errors found.
          </div>
        )}
        <TableFooter pagination={pagination} handlePageChange={handlePageChange} />
      </div>
    </div>
  );
};

export default Errors;
