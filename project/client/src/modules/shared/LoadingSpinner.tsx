// packages
import { Spinner } from "react-bootstrap";

interface LoadingSpinnerProps {
  message: string;
}

const LoadingSpinner = ({ message } : LoadingSpinnerProps) => {
  return (
    <div className="loading-container">
      <Spinner animation="border" variant="primary" className="spinner" />
      <span className="loading-text">{message}</span>
    </div>
  );
};

export default LoadingSpinner;
