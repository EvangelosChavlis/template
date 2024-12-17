import { useRef, useState } from "react";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Container from "react-bootstrap/Container";

// source
import Header from "src/modules/shared/Header";
import { ButtonProps } from "src/models/shared/buttonProps";
import ValidationErrorMessage from "src/modules/shared/ValidationErrorMessage";
import { PickerWarningDto } from "src/models/weather/warningsDto";
import useUpdate from "src/modules/forecasts/pages/Update/useUpdate";
import { formatDateToISO } from "src/utils/utils";

const Update = () => {
  const { handleSubmit, warningsPicker, form } = useUpdate();
  const [errors, setErrors] = useState<{
    date?: string;
    temperatureC?: string;
    summary?: string;
    warningId?: string;
  }>({});
  
  const [selectedWarningId, setSelectedWarningId] = useState<string>(form.warningId);
  const dateRef = useRef<HTMLInputElement>(null);
  const temperatureCRef = useRef<HTMLInputElement>(null);
  const summaryRef = useRef<HTMLTextAreaElement>(null);

  const header = "Update Forecast";


  const onSubmit = async () => {
    try {
      setErrors({});

      await handleSubmit({
        preventDefault: () => {},
        date: dateRef.current?.value ?? "",
        temperatureC: parseFloat(temperatureCRef.current?.value ?? "0"),
        summary: summaryRef.current?.value ?? "",
        warningId: selectedWarningId, // Use the selected warning ID
      });
    } catch (validationErrors) {
      if (typeof validationErrors === "object") {
        setErrors(validationErrors!);
      }
    }
  };

  const buttons: ButtonProps[] = [
    {
      title: "Submit",
      action: onSubmit,
      icon: <i className="bi bi-check"></i>,
      color: "success",
      placement: "top",
      disabled: false,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2" style={{ flex: 1, overflowY: "auto" }}>
        <Container className="mt-4">
          <Form
            onSubmit={(e) => {
              e.preventDefault();
              onSubmit();
            }}
          >
            <InputGroup className="mb-3 mt-2">
              <InputGroup.Text>
                <i className="bi bi-calendar"></i>
              </InputGroup.Text>
              <InputGroup.Text id="date">Date</InputGroup.Text>
              <Form.Control
                name="date"
                ref={dateRef}
                defaultValue={formatDateToISO(form.date)}
                type="date"
                placeholder="Select a date..."
                aria-label="date"
                aria-describedby="date"
                className={errors.date ? "is-invalid" : "is-valid"}
                required
              />
            </InputGroup>
            <ValidationErrorMessage message={errors.date} />

            <InputGroup className="mb-3 mt-5">
              <InputGroup.Text>
                <i className="bi bi-thermometer-half"></i>
              </InputGroup.Text>
              <InputGroup.Text id="temperatureC">Temperature (°C)</InputGroup.Text>
              <Form.Control
                name="temperatureC"
                ref={temperatureCRef}
                defaultValue={form.temperatureC}
                type="number"
                placeholder="Enter temperature..."
                aria-label="temperatureC"
                aria-describedby="temperatureC"
                className={errors.temperatureC ? "is-invalid" : "is-valid"}
                required
              />
            </InputGroup>
            <ValidationErrorMessage message={errors.temperatureC} />

            <InputGroup className="mb-3 mt-5">
              <InputGroup.Text>
                <i className="bi bi-chat-dots"></i>
              </InputGroup.Text>
              <InputGroup.Text id="summary">Summary</InputGroup.Text>
              <Form.Control
                name="summary"
                defaultValue={form.summary}
                ref={summaryRef}
                as="textarea"
                placeholder="Enter a summary..."
                aria-label="summary"
                className={errors.summary ? "is-invalid" : "is-valid"}
                required
              />
            </InputGroup>
            <ValidationErrorMessage message={errors.summary} />

            <InputGroup className="mb-3 mt-5">
              <InputGroup.Text>
                <i className="bi bi-exclamation-circle"></i>
              </InputGroup.Text>
              <InputGroup.Text id="warningId">Warning</InputGroup.Text>
              <Form.Control
                as="select"
                name="warningId"
                value={selectedWarningId}
                onChange={(e) => setSelectedWarningId(e.target.value)}
                aria-label="warningId"
                aria-describedby="warningId"
                className={errors.warningId ? "is-invalid" : "is-valid"}
                required
              >
                <option value="" disabled> Select a warning... </option>
                {warningsPicker?.data!.map((warning: PickerWarningDto) => (
                  <option key={warning.id} value={warning.id}>
                    {warning.name}
                  </option>
                ))}
              </Form.Control>
            </InputGroup>
            <ValidationErrorMessage message={errors.warningId} />
          </Form>
        </Container>
      </div>
    </div>
  );
};

export default Update;
