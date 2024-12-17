// packages
import { Spinner } from "react-bootstrap";

const LoadingSpinner = () => {
  return (
    <div className="loading-container">
      <Spinner animation="border" variant="primary" className="spinner" />
      <span className="loading-text">Loading...</span>
    </div>
  );
};

export default LoadingSpinner;
