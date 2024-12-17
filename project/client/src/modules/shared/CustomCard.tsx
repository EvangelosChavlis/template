// packages
import Card  from "react-bootstrap/Card"
import Button  from "react-bootstrap/Button"
import Tooltip  from "react-bootstrap/Tooltip"
import OverlayTrigger  from "react-bootstrap/OverlayTrigger"

interface CardProps {
  title: string;
  iconClass: string;
  text: string;
  buttonLabel: string;
  buttonLink: string;
  buttonVariant: string;
  tooltip: string;
}

const CustomCard = ({
  title,
  iconClass,
  text,
  buttonLabel,
  buttonLink,
  buttonVariant,
  tooltip,
}: CardProps) => {
  return (
    <Card className="shadow border-0 h-100 hover-card">
      <Card.Body>
        <Card.Title className={`fw-bold text-${buttonVariant}`}>
          <i className={iconClass} /> {title}
        </Card.Title>
        <Card.Text>{text}</Card.Text>
        <OverlayTrigger placement="bottom" overlay={<Tooltip>{tooltip}</Tooltip>}>
          <Button
            variant={buttonVariant}
            href={buttonLink}
            className="d-flex align-items-center justify-content-center"
          >
            <i className={`bi bi-${buttonVariant === "primary" ? "eye" : "bell"} me-2`} />
            {buttonLabel}
          </Button>
        </OverlayTrigger>
      </Card.Body>
    </Card>
  );
};

export default CustomCard;
