// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Badge from "react-bootstrap/Badge";

// source
import useForecasts from "src/modules/forecasts/pages/Table/useForecasts";
import { ButtonProps } from "src/models/shared/buttonProps";
import Header from "src/modules/shared/Header";
import { ListItemForecastDto } from "src/models/weather/forecastsDto";
import { getBadgeColor, incNumberFunction } from "src/utils/utils";
import TableFooter from "src/modules/shared/TableFooter";
import LoadingSpinner from "src/modules/shared/LoadingSpinner";


const Forecasts = () => {
  const navigate = useNavigate();

  const {
    forecasts,
    pagination,
    handlePageChange,
  } = useForecasts();

  const navigateClick = (forecastId: string) => {
    navigate(`/forecasts/${forecastId}`);
  };

  if (!forecasts) return <LoadingSpinner />;

  const header = "Forecasts Page";
  const buttons: ButtonProps[] = [
    {
      title: "Create Forecast",
      action: () => navigate("create"),
      icon: <i className="bi bi-plus-square"></i>,
      color: "primary",
      placement: "top",
      disabled: false,
    },
    {
      title: "Initialize Forecasts",
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
        {forecasts.length > 0 ? (
          <Table
            responsive
            hover
            className="shadow-sm"
            style={{
              borderCollapse: "collapse",
            }}>
            <thead className="bg-primary text-white">
              <tr>
                <th className="text-center" style={{ width: "5%" }}>#</th>
                <th className="text-center" style={{ width: "30%" }}>
                  <i className="bi bi-calendar3 icon-margin-right"/>
                  <span>Date</span>
                  </th>
                <th className="text-center" style={{ width: "20%" }}>
                  <i className="bi bi-thermometer icon-margin-right"/>
                  <span>Temperature (°C)</span>
                </th>
                <th className="text-center" style={{ width: "15%" }}>
                  <i className="bi bi-exclamation-triangle icon-margin-right"/>
                  <span>Warning</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {forecasts.map((forecast: ListItemForecastDto, idx) => (
                <tr key={forecast.id}>
                  <td className="text-center">
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center">
                    <Button
                      variant="link"
                      className="text-decoration-none text-primary fw-bold"
                      onClick={() => navigateClick(forecast.id)}
                    >
                      {forecast.date}
                    </Button>
                  </td>
                  <td className="text-center">
                    <Badge bg="info" text="dark">
                      {forecast.temperatureC}
                    </Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg={getBadgeColor(forecast.warning)} pill>
                      {forecast.warning}
                    </Badge>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          <div className="alert alert-warning text-center">
            No forecasts found.
          </div>
        )}
        <TableFooter pagination={pagination} handlePageChange={handlePageChange} />
      </div>
    </div>
  );
};

export default Forecasts;