// packages
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

// source
import CustomCard from "src/modules/shared/CustomCard";

const Home = () => {
  return (
    <div className="bg-light" style={{ minHeight: "88vh" }}>
      <Container className="mt-5">
        <header className="text-center mb-5">
          <h1 className="fw-bold text-primary">Weather App Template 🌐</h1>
          <p className="lead text-muted mt-4">
            A powerful starting point for building weather applications and development platforms.
          </p>
        </header>

        <Row className="g-4">
          <Col md={6} lg={4}>
            <CustomCard
              title="Forecasts"
              iconClass="bi bi-wind"
              text="Explore detailed weather forecasts for multiple days to plan ahead effectively."
              buttonLabel="View Forecasts"
              buttonLink="/forecasts"
              buttonVariant="primary"
              tooltip="View detailed weather forecasts"
            />
          </Col>

          <Col md={6} lg={4}>
            <CustomCard
              title="Warnings"
              iconClass="bi bi-exclamation-triangle"
              text="Stay informed with real-time alerts and warnings about severe weather conditions."
              buttonLabel="View Warnings"
              buttonLink="/warnings"
              buttonVariant="warning"
              tooltip="Get real-time weather warnings"
            />
          </Col>

          <Col md={6} lg={4}>
            <CustomCard
              title="Telemetry"
              iconClass="bi bi-bar-chart-line"
              text="Monitor telemetry data to ensure optimal performance and user experience."
              buttonLabel="View Telemetry"
              buttonLink="/telemetry"
              buttonVariant="success"
              tooltip="Monitor telemetry and performance data"
            />
          </Col>

          <Col md={6} lg={4}>
            <CustomCard
              title="Log Errors"
              iconClass="bi bi-bug"
              text="Monitor application errors and logs to troubleshoot and optimize your app."
              buttonLabel="View Log Errors"
              buttonLink="/log-errors"
              buttonVariant="danger"
              tooltip="Track and fix application errors"
            />
          </Col>

          <Col md={6} lg={4}>
            <CustomCard
              title="Users"
              iconClass="bi bi-person-circle"
              text="Manage users securely for your app’s authentication system."
              buttonLabel="Manage Users"
              buttonLink="/users"
              buttonVariant="info"
              tooltip="Manage app users and authentication"
            />
          </Col>

          <Col md={6} lg={4}>
            <CustomCard
              title="Roles"
              iconClass="bi bi-shield-lock"
              text="Manage roles and permissions for a secure authentication system."
              buttonLabel="Manage Roles"
              buttonLink="/roles"
              buttonVariant="secondary"
              tooltip="Manage roles and permissions"
            />
          </Col>

          <Col md={6} lg={4}>
            <CustomCard
              title="Development Template"
              iconClass="bi bi-code-slash"
              text="Use this app as a starting point with customizable modules for your own apps."
              buttonLabel="View Documentation"
              buttonLink="http://localhost:5000/swagger/index.html"
              buttonVariant="dark"
              tooltip="Access app documentation for developers"
            />
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default Home;
