import { Link, useNavigate } from "react-router-dom";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import { Button, OverlayTrigger, Tooltip } from "react-bootstrap";

// source
import IconWithText from "src/modules/shared/IconWithText";
import useHome from "src/modules/home/pages/useHome";
import useAuth from "src/utils/useAuth";

const Menu = () => {
  const { handleClearData, handleSeedData } = useHome();
  const { user, roles, isAdministrator, isManager } = useAuth();
  const navigate = useNavigate();
  
  const handleLogout = () => {
    localStorage.removeItem("authToken");
    localStorage.removeItem("userName");  
    navigate("/auth/login");
  };

  return (
    <Navbar bg="primary" variant="dark">
      <Container>
        <Navbar.Brand as={Link} to="/">
          <i className="bi bi-cloud-sun"></i> Weather app
        </Navbar.Brand>
        <Nav className="me-auto">
          {isAdministrator() && (
            <NavDropdown
              title={<IconWithText iconClass="bi bi-person-lock" text="Auth" />}
              id="nav-dropdown"
            >
              <NavDropdown.Item as={Link} to="/users" eventKey="1">
                <IconWithText iconClass="bi bi-person-circle mr-2" text="Users" />
              </NavDropdown.Item>
              <NavDropdown.Item as={Link} to="/roles" eventKey="2">
                <IconWithText iconClass="bi bi-shield-lock" text="Roles" />
              </NavDropdown.Item>
            </NavDropdown>
          )}
          <Nav.Link as={Link} to="/forecasts">
            <IconWithText iconClass="bi bi-wind" text="Forecasts" />
          </Nav.Link>

          {(isAdministrator() || isManager()) && (
            <Nav.Link as={Link} to="/warnings">
              <IconWithText iconClass="bi bi-exclamation-triangle" text="Warnings" />
            </Nav.Link>
          )}
          
          {isAdministrator() && (
            <NavDropdown
              title={<IconWithText iconClass="bi bi-activity me-2" text="Metrics" />}
              id="nav-dropdown"
            >
              <NavDropdown.Item as={Link} to="/telemetry" eventKey="3">
                <IconWithText iconClass="bi bi-bar-chart-line" text="Telemetry" />
              </NavDropdown.Item>
              <NavDropdown.Item as={Link} to="/errors" eventKey="4">
                <IconWithText iconClass="bi bi-bug" text="Log Errors" />
              </NavDropdown.Item>
            </NavDropdown>
          )}
        </Nav>

        {isAdministrator() && (
          <div style={{ display: "inline-block", marginRight: "5px" }}>
            <OverlayTrigger
              placement="bottom"
              overlay={<Tooltip id="tooltip-seed">Seed Data</Tooltip>}
            >
              <Button style={{ marginRight: "0.5rem" }} variant="light" onClick={() => handleSeedData()}>
                <i className="bi bi-plus-circle-fill" />
              </Button>
            </OverlayTrigger>

            <OverlayTrigger
              placement="bottom"
              overlay={<Tooltip id="tooltip-clear">Clear Data</Tooltip>}
            >
              <Button variant="danger" onClick={() => handleClearData()}>
                <i className="bi bi-x-circle-fill" />
              </Button>
            </OverlayTrigger>
          </div>
        )}

        {user && (
          <NavDropdown
            title={<span style={{ color: "white" }}>{user.email}</span>}
            id="user-dropdown"
            align="end"
          >
            <NavDropdown.Item>
              <strong>Email:</strong> {user.email}
            </NavDropdown.Item>
            <NavDropdown.Item>
              <strong>Roles:</strong> {roles.length > 0 ? roles.join(", ") : "No roles assigned"}
            </NavDropdown.Item>
            <NavDropdown.Divider />
            <NavDropdown.Item onClick={handleLogout}>
              <IconWithText iconClass="bi bi-box-arrow-right" text="Logout" />
            </NavDropdown.Item>
          </NavDropdown>
        )}
      </Container>
    </Navbar>
  );
};

export default Menu;
