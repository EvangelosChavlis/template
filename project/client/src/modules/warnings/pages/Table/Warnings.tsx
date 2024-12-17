// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Badge from "react-bootstrap/Badge";

// source
import useWarnings from "src/modules/warnings/pages/Table/useWarnings";
import { ButtonProps } from "src/models/shared/buttonProps";
import { ListItemWarningDto } from "src/models/weather/warningsDto";
import { getBadgeColor, incNumberFunction } from "src/utils/utils";
import TableFooter from "src/modules/shared/TableFooter";
import Header from "src/modules/shared/Header";

const Warnings = () => {
  const navigate = useNavigate();

  const {
    warnings,
    pagination,
    handlePageChange,
  } = useWarnings();

  const navigateClick = (warningId: string) => {
    navigate(`/warnings/${warningId}`);
  };

  const header = "Warnings Page";
  const buttons: ButtonProps[] = [
    {
      title: "Create Warning",
      action: () => navigate("create"),
      icon: <i className="bi bi-plus-square"></i>,
      color: "primary",
      placement: "top",
      disabled: false,
    },
    {
      title: "Initialize Warnings",
      action: () => navigate("initialize"),
      icon: <i className="bi bi-folder-plus"></i>,
      color: "success",
      placement: "top",
      disabled: false,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        {warnings.length > 0 ? (
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
                <th className="text-center" style={{ width: "30%" }}>
                  <i className="bi bi-chat icon-margin-right"/>
                  <span>Name</span>
                </th>
                <th className="text-center" style={{ width: "50%" }}>
                  <i className="bi bi-chat-dots icon-margin-right" />
                  <span>Description</span>
                </th>
                <th className="text-center" style={{ width: "15%" }}>
                  <i className="bi bi-cloud-haze-fill icon-margin-right" />
                  <span>Forecasts</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {warnings.map((warning: ListItemWarningDto, idx) => (
                <tr key={warning.id}>
                  <td className="text-center">
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center">
                    <Button
                      variant="link"
                      className={`text-decoration-none text-${getBadgeColor(warning.name)} fw-bold`}
                      onClick={() => navigateClick(warning.id)}
                    >
                      {warning.name}
                    </Button>
                  </td>
                  <td className="text-center">{warning.description}</td>
                  <td className="text-center">
                    <Badge bg="dark" pill>
                      {warning.count}
                    </Badge>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          <div className="alert alert-warning text-center">
            No warnings found.
          </div>
        )}
        <TableFooter pagination={pagination} handlePageChange={handlePageChange} />
      </div>
    </div>
  );
};

export default Warnings;