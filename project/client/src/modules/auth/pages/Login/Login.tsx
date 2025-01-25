// packages
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Alert from "react-bootstrap/Alert";
import InputGroup from "react-bootstrap/InputGroup";

// source
import UseLogin from "src/modules/auth/pages/Login/useLogin";
import Footer from "src/modules/shared/Footer";

const Login = () => {
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

  const { username, password, error, setUsername, setPassword, handleLogin } = UseLogin();

  const handleShowPasswordToggle = () => setShowPassword((prev) => !prev);

  return (
    <Container fluid className="d-flex justify-content-center align-items-center bg-primary vh-100">
      <Row className="w-100">
        <Col md={{ span: 6, offset: 3 }}>
          <div className="bg-white border rounded p-5 shadow-lg">
            <h2 className="text-center mb-4 text-primary">Weather App</h2>
            <h4 className="text-center mb-4 text-primary">Login</h4>

            {error && <Alert variant="danger" className="text-center">{error}</Alert>}

            <Form onSubmit={handleLogin}>
              <Form.Group controlId="formUsername" className="mb-4">
                <Form.Label className="fw-bold">Username</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter your username"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  required
                  className="py-2"
                  aria-label="Username"
                />
              </Form.Group>

              <Form.Group controlId="formPassword" className="mb-4">
                <Form.Label className="fw-bold">Password</Form.Label>
                <InputGroup>
                  <Form.Control
                    type={showPassword ? "text" : "password"}
                    placeholder="Enter your password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    className="py-2"
                    aria-label="Password"
                  />
                  <InputGroup.Text
                    onClick={handleShowPasswordToggle}
                    role="button"
                    aria-pressed={showPassword}
                    title={showPassword ? "Hide password" : "Show password"}
                    style={{ cursor: "pointer" }}
                  >
                    <i className={`bi ${showPassword ? "bi-eye-slash" : "bi-eye"}`} />
                  </InputGroup.Text>
                </InputGroup>
              </Form.Group>

              <Button variant="primary" type="submit" className="w-100 py-2 fw-bold">
                Login
              </Button>
            </Form>

            <div className="text-center mt-4">
              <Button
                variant="link"
                className="text-decoration-none text-primary fw-bold"
                onClick={() => navigate("/auth/register")}
              >
                Don&apos;t have an account? Register
              </Button>
            </div>
          </div>
        </Col>
        <Footer />
      </Row>
    </Container>
  );
};

export default Login;
