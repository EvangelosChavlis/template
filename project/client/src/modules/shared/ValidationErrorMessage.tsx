// source
import { ValidationProps } from "src/models/shared/validationProps";

const ValidationErrorMessage = ({ message }: ValidationProps) => {
  return (
    <div
      className={`mt-1 ${message ? 'text-danger' : 'text-success'}`}
    >
      <i className={`bi ${message ? 'bi-exclamation-circle-fill' : 'bi-check-circle-fill'}`} />{" "}
      <span>{message || "Looks good!"}</span>
    </div>
  );
};

export default ValidationErrorMessage;
