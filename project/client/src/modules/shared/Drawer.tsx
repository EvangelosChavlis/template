// packages
import { useState } from "react";
import Offcanvas from "react-bootstrap/Offcanvas";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import OverlayTrigger from "react-bootstrap/OverlayTrigger";
import Tooltip from "react-bootstrap/Tooltip";
import { toast } from "react-toastify";

// source
import { ItemRoleDto } from "src/models/auth/roleDto";

interface RoleDrawerProps {
  roles: ItemRoleDto[];
  show: boolean;
  onClose: () => void;
  userId: string;
  currentRoles: string[];
  handleAssignRoleToUser: (userId: string, roleId: string) => void;
  handleUnassignRoleFromUser: (userId: string, roleId: string) => void;
}

const Drawer = ({ 
    roles, 
    show, 
    onClose, 
    userId, 
    currentRoles,
    handleAssignRoleToUser,
    handleUnassignRoleFromUser,
}: RoleDrawerProps) => {
  const [selectedRole, setSelectedRole] = useState<string>("");

  const handleRoleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedRole(event.target.value);
  };

  const handleSave = async () => {
    try {
      // Assign the selected role to the user
      if (selectedRole) {
        handleAssignRoleToUser(userId, selectedRole);
        onClose();
      }
    } catch (error) {
      toast.error("Failed to assign role.");
    }
  };

  const handleUnassignRole = async (roleName: string) => {
    try {
      // Unassign the selected role from the user
      let roleId = roles.find(r => r.name === roleName)?.id!;
      handleUnassignRoleFromUser(userId, roleId);
      onClose();
    } catch (error) {
      toast.error("Failed to unassign role.");
    }
  };

  return (
    <Offcanvas show={show} onHide={onClose} placement="end" style={{ width: "50%" }}>
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Assign Role</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body>
        <section>
          <h5>Select a Role</h5>
          <p className="text-muted">Assign a new role to the user from the list below:</p>
          <Form.Group controlId="roleSelect" className="mb-4">
            <Form.Label className="fw-semibold">Available Roles</Form.Label>
            <Form.Select value={selectedRole} onChange={handleRoleChange}>
              <option value="" disabled>
                Choose a role...
              </option>
              {roles.map((role) => (
                <option
                  key={role.id}
                  value={role.id}
                  disabled={currentRoles.includes(role.name)}
                >
                  {role.name} - {role.description}
                </option>
              ))}
            </Form.Select>
          </Form.Group>
        </section>

        <section className="mt-4">
          <h5>Current Roles</h5>
          {currentRoles.length === 0 ? (
            <p className="text-muted">No roles assigned to this user.</p>
          ) : (
            <ul className="list-group">
              {currentRoles.map((role, index) => (
                <li
                    key={index}
                    className={`list-group-item bg-light d-flex justify-content-between align-items-center`}
                >
                <span>{role}</span>
                <OverlayTrigger
                    placement="left"
                    overlay={
                    <Tooltip id={`tooltip-unassing`} placement="bottom">
                        <strong>Unassign</strong>
                    </Tooltip>
                    }
                >
                    <Button variant="danger" onClick={() => handleUnassignRole(role)} className="me-2">
                    <i className="bi bi-trash" />
                    </Button>
                </OverlayTrigger>
                </li>
              ))}
            </ul>
          )}
        </section>

        <div className="mt-4 d-flex justify-content-end">
          <OverlayTrigger
            placement="bottom"
            overlay={
              <Tooltip id={`tooltip-cancel`} placement="bottom">
                <strong>Cancel</strong>
              </Tooltip>
            }
          >
            <Button variant="secondary" onClick={onClose} className="me-2">
                <i className="bi bi-ban" />
            </Button>
          </OverlayTrigger>

          <OverlayTrigger
            placement="bottom"
            overlay={
              <Tooltip id={`tooltip-save`} placement="bottom">
                <strong>Assign Role</strong>
              </Tooltip>
            }
          >
            <Button
                disabled={!selectedRole}
                variant="primary"
                onClick={handleSave}
            >
                <i className="bi bi-plus-square" />
            </Button>
          </OverlayTrigger>
        </div>
      </Offcanvas.Body>
    </Offcanvas>
  );
};

export default Drawer;
