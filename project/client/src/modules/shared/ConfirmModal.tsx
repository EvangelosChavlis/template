// packages
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import OverlayTrigger from "react-bootstrap/OverlayTrigger";
import Tooltip from "react-bootstrap/Tooltip";

// source
import { ConfirmModalProps } from "src/models/shared/confirmModalProps";

const ConfirmModal = ({
  show,
  onClose,
  onConfirm,
  title,
  message,
}: ConfirmModalProps) => {
  return (
    <Modal show={show} onHide={onClose}>
      <Modal.Header closeButton>
        <Modal.Title>{title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>{message}</Modal.Body>
      <Modal.Footer>
      < OverlayTrigger
          placement="top"
          overlay={<Tooltip id="tooltip-cancel">Cancel</Tooltip>}
        >
          <Button variant="secondary" onClick={onConfirm}>
            <i className="bi bi-ban" />
          </Button>
        </OverlayTrigger>

        <OverlayTrigger
          placement="top"
          overlay={<Tooltip id="tooltip-delete">Delete</Tooltip>}
        >
          <Button variant="danger" onClick={onConfirm}>
            <i className="bi bi-trash3" />
          </Button>
        </OverlayTrigger>
      </Modal.Footer>
    </Modal>
  );
};

export default ConfirmModal;
