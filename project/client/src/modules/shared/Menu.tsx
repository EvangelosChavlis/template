// packages
import { Link } from "react-router-dom";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";

// source
import IconWithText from "src/modules/shared/IconWithText";

const Menu = () => {
  return (
    <Navbar bg="primary" variant="dark">
      <Container>
        <Navbar.Brand as={Link} to="/">
          <i className="bi bi-cloud-sun"></i> Weather app
        </Navbar.Brand>
        <Nav className="me-auto">
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
          
          <Nav.Link as={Link} to="/forecasts">
            <IconWithText iconClass="bi bi-wind" text="Forecasts" />
          </Nav.Link>
          
          <Nav.Link as={Link} to="/warnings">
            <IconWithText iconClass="bi bi-exclamation-triangle" text="Warnings" />
          </Nav.Link>
          
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
        </Nav>
      </Container>
    </Navbar>
  );
};

export default Menu;
