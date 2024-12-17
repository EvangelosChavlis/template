// packages
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Badge from "react-bootstrap/Badge";
import OverlayTrigger from "react-bootstrap/OverlayTrigger";
import Row from "react-bootstrap/Row";
import Tooltip from "react-bootstrap/Tooltip";

// source
import { HeaderProps } from "src/models/shared/headerProps";


const Header = ({ header, buttons, counter }: HeaderProps) => {
  return (
    <Row className="align-items-center mb-2">
      <Col>
        <h2>
          <i
            className="bi bi-caret-right-fill text-danger me-2"
            style={{ fontSize: "1.5rem" }}
          ></i>
          <span>{header}</span>{" "}
          {counter && (
            <Badge pill bg="success" style={{ fontSize: "0.7rem" }}>
              {counter}
            </Badge>
          )}
        </h2>
      </Col>
      <Col className="text-end">
        {buttons.map((button, idx) => (
          <OverlayTrigger
            key={idx}
            placement="top"
            overlay={
              <Tooltip id={`tooltip-${idx}`} placement={button.placement}>
                <strong>{button.title}</strong>
              </Tooltip>
            }
          >
            <div style={{ display: "inline-block", marginRight: "5px" }}>
              <Button
                disabled={button.disabled}
                variant={button.color}
                onClick={button.action}
              >
                {button.icon}
              </Button>
            </div>
          </OverlayTrigger>
        ))}
      </Col>
    </Row>
  );
};

export default Header;
