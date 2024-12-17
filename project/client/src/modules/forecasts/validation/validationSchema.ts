// packages
import * as yup from "yup";

// Forecast Schema
const forecastSchema = yup.object().shape({
  date: yup
    .date()
    .required("Date is required.")
    .min(new Date("0001-01-01T00:00:00Z"), "Date must be a valid date."),
  temperatureC: yup
    .number()
    .required("TemperatureC is required.")
    .min(-50, "TemperatureC must be greater than or equal to -50.")
    .max(50, "TemperatureC must be less than or equal to 50."),
  summary: yup
    .string()
    .required("Summary is required.")
    .max(200, "Summary must not exceed 200 characters."),
  warningId: yup
    .string()
    .required("WarningId is required.")
    .matches(
      /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/,
      "WarningId must be a valid GUID."
    ),
});

export default forecastSchema;
