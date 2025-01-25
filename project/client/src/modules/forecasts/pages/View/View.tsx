// packages
import Badge from "react-bootstrap/Badge";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import useView from "src/modules/forecasts/pages/View/useView";
import Header from "src/modules/shared/Header";
import { getBadgeColor } from "src/utils/utils";

const View = () => {
  const {
    forecast,
    handleDelete,
    navigate,
    id,
  } = useView();

  
  const header = "Forecast Info";

  const buttons: ButtonProps[] = [
    {
      title: "Update Forecast",
      action: () => navigate(`/forecasts/update/${id}`),
      icon: <i className="bi bi-pencil-square"></i>,
      color: "warning",
      placement: "top",
      disabled: false,
    },
    {
      title: "Delete Forecast",
      action: () => handleDelete(),
      icon: <i className="bi bi-trash3"></i>,
      color: "danger",
      placement: "top",
      disabled: false,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div
        className="p-3 border rounded bg-light mt-2"
        style={{ flex: 1, overflowY: "auto" }}
      >
        <div className="mt-2">
          <strong className="strong-margin-right">
            <i className="bi bi-calendar3 icon-margin-right"/> Date
          </strong>
          <span>{forecast.date}</span>
        </div>
        <div className="mt-5">
          <strong className="strong-margin-right">
            <i className="bi bi-thermometer icon-margin-right"/> Temperature (°C)
          </strong>
          <span>{forecast.temperatureC}</span>
        </div>
        <div className="mt-5">
          <strong className="strong-margin-right">
            <i className="bi bi-chat-dots icon-margin-right"/> Summary
          </strong>
          <span>{forecast.summary}</span>
        </div>
        <div className="mt-5 mb-4">
          <strong className="strong-margin-right">
            <i className="bi bi-exclamation-triangle icon-margin-right"/> Warning
          </strong>
          <Badge bg={getBadgeColor(forecast.warning)} pill>
            {forecast.warning}
          </Badge>
        </div>
      </div>
    </div>
  );
};

export default View;
