// packages
import Form from "react-bootstrap/Form";
import Table from "react-bootstrap/Table";
import ListGroup from "react-bootstrap/ListGroup";

// source
import { ButtonProps } from "src/models/shared/buttonProps";

import Header from "src/modules/shared/Header";
import useInitialize from "src/modules/forecasts/pages/Initialize/useInitialize";
import { PickerWarningDto } from "src/models/weather/warningsDto";

const Initialize = () => {
  const { 
    formDataList, 
    currentForecast, 
    handleChange, 
    addForm, 
    removeForm, 
    submitForecasts,
    warningsPicker 
  } = useInitialize();


  const header = "Initialize Forecasts";
  const buttons: ButtonProps[] = [
    {
      title: "Add Forecast",
      action: addForm,
      icon: <i className="bi bi-plus"></i>,
      color: "primary",
      placement: "top",
      disabled: false,
    },
    {
      title: "Submit",
      action: submitForecasts,
      icon: <i className="bi bi-check"></i>,
      color: "success",
      placement: "top",
      disabled: formDataList.length === 0,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />

      <ListGroup className="mb-4">
        {formDataList.map((forecast, idx) => (
          <ListGroup.Item
            key={idx}
            className="p-3 border rounded bg-light m-2 d-flex flex-column"
            style={{ flex: 1, overflowY: "auto" }}
          >
            <div className="mt-1">
              <strong className="strong-margin-right">
                <i className="bi bi-calendar3 icon-margin-right" /> Date
              </strong>
              <span>{forecast.date}</span>
            </div>
            <div className="mt-2">
              <strong className="strong-margin-right">
                <i className="bi bi-thermometer icon-margin-right" /> Temperature
              </strong>
              <span>{forecast.temperatureC}</span>
            </div>
            <div className="mt-2">
              <strong className="strong-margin-right">
                <i className="bi bi-chat-dots icon-margin-right" /> Temperature
              </strong>
              <span>{forecast.summary}</span>
            </div>
            <div className="mt-2">
              <strong className="strong-margin-right">
                <i className="bi bi-exclamation-triangle icon-margin-right" /> Warning
              </strong>
              <span>{warningsPicker?.data!.find(i => i.id === forecast.warningId)?.name}</span>
            </div>
            <div className="d-flex justify-content-end">
              <i onClick={() => removeForm(idx)} className="bi bi-trash3-fill text-danger" />
            </div>
          </ListGroup.Item>
        ))}
      </ListGroup>

      <Form className="p-3 border rounded mt-2">
        <Table
          responsive
          hover
          className="shadow-sm"
          style={{
            borderCollapse: "collapse",
          }}
        >
          <thead>
            <tr>
              <th>Date</th>
              <th>Temperature</th>
              <th>Summary</th>
              <th>Warning</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>
                <Form.Control
                  name="date"
                  defaultValue={new Date().toISOString().split("T")[0]}
                  value={currentForecast.date}
                  type="date"
                  onChange={handleChange}
                  aria-label="date"
                />
              </td>
              <td>
                <Form.Control
                  name="temperatureC"
                  defaultValue={15}
                  value={currentForecast.temperatureC}
                  type="number"
                  onChange={handleChange}
                  placeholder="Enter temperature..."
                  aria-label="temperatureC"
                />
              </td>
              <td>
                <Form.Control
                  name="summary"
                  value={currentForecast.summary}
                  onChange={handleChange}
                  as="textarea"
                  placeholder="Enter a summary..."
                  aria-label="summary"
                />
              </td>
              <td>
                <Form.Control
                  as="select"
                  name="warningId"
                  value={currentForecast.warningId}
                  onChange={handleChange}
                  aria-label="warningId"
                >
                  <option value="" disabled> Select a warning... </option>
                  {warningsPicker?.data?.map((warning: PickerWarningDto) => (
                    <option key={warning.id} value={warning.id}>
                      {warning.name}
                    </option>
                  ))}
                </Form.Control>
              </td>
            </tr>
          </tbody>
        </Table>
      </Form>
    </div>
  );
};

export default Initialize;
