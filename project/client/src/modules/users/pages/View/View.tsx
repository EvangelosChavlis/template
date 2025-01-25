// packages
import { useState } from "react";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import Header from "src/modules/shared/Header";
import useView from "src/modules/users/pages/View/useView";
import ConfirmModal from "src/modules/shared/ConfirmModal";
import Drawer from "src/modules/shared/Drawer";
import { Badge, Button, Table } from "react-bootstrap";
import Details from "src/modules/users/pages/View/components/TabDetails";
import TabDetails from "src/modules/users/pages/View/components/TabDetails";
import TabContactInfo from "src/modules/users/pages/View/components/TabContactInfo";
import TabSystemInfo from "src/modules/users/pages/View/components/TabSystemInfo";

const View = () => {
  const {
    user,
    roles,
    handleDelete,
    handleActivateUser,
    handleDeactivateUser,
    handleLockUser,
    handleUnlockUser,
    handleGeneratePasswordUser,
    handleAssignRoleToUser,
    handleUnassignRoleFromUser,
    navigate,
    id,
  } = useView();

  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const handleShowDeleteModal = () => setShowDeleteModal(true);
  const handleCloseDeleteModal = () => setShowDeleteModal(false);

  const [showRoleDrawer, setShowRoleDrawer] = useState(false);
  const handleOpenRoleDrawer = () => setShowRoleDrawer(true);
  const handleCloseRoleDrawer = () => setShowRoleDrawer(false);

  const handleConfirmDelete = () => {
    handleDelete();
    setShowDeleteModal(false);
  };

  const header = "User Info";
  const buttons: ButtonProps[] = [
    {
      title: "Update User",
      action: () => navigate(`/users/update/${id}`),
      icon: <i className="bi bi-pencil-square" />,
      color: "warning",
      placement: "top",
      disabled: false
    },
    {
      title: "Delete User",
      action: handleShowDeleteModal,
      icon: <i className="bi bi-trash3" />,
      color: "danger",
      placement: "top",
      disabled: false
    },
    {
      title: "Lock User",
      action: () => handleLockUser(),
      icon: <i className="bi bi-lock-fill" />,
      color: "info",
      placement: "top",
      disabled: user.lockoutEnabled,
    },
    {
      title: "Unlock User",
      action: () => handleUnlockUser(),
      icon: <i className="bi bi-unlock-fill" />,
      color: "info",
      placement: "top",
      disabled: !user.lockoutEnabled,
    },
    {
      title: "Activate User",
      action: () => handleActivateUser(),
      icon: <i className="bi bi-check-circle-fill" />,
      color: "info",
      placement: "top",
      disabled: user.isActive,
    },
    {
      title: "Deactivate User",
      action: () => handleDeactivateUser(),
      icon: <i className="bi bi-x-circle-fill" />,
      color: "info",
      placement: "top",
      disabled: !user.isActive,
    },
    {
      title: "Reset Password",
      action: () => handleGeneratePasswordUser(),
      icon: <i className="bi bi-arrow-clockwise" />,
      color: "info",
      placement: "top",
      disabled: false,
    },
    {
      title: "Manage Roles",
      action: () => handleOpenRoleDrawer(),
      icon: <i className="bi bi-shield-lock" />,
      color: "secondary",
      placement: "top",
      disabled: false,
    }
  ];

  return (
    <div className="container mt-4 mb-4">
      <Header header={header} buttons={buttons} />

      <Row className="mt-2" style={{ flex: 1 }}>
        <Col style={{ display: "flex", flexDirection: "column" }}>
          <Tabs defaultActiveKey="details" id="user-tabs" className="h-100">
            <Tab
              eventKey="details"
              title={
              <span>
                  <i className="bi bi-info-circle-fill"></i> Details
              </span>
              }
            >
            <TabDetails user={user}/>
          </Tab>
          <Tab
            eventKey="contact-info"
            title={
              <span>
                <i className="bi bi-telephone-fill" /> Contact Info
              </span>
            }
          >
            <TabContactInfo user={user}/>
          </Tab>
            <Tab
              eventKey="system-info"
              title={
                <span>
                  <i className="bi bi-gear-fill" /> System Info
                </span>
              }
            >
              <TabSystemInfo user={user}/>
            </Tab>
            <Tab
              eventKey="roles"
              title={
                <span>
                  <i className="bi bi-people-fill" /> Roles
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                {/* {user.roles.length > 0 ? (
                  <ul>
                    {user.roles.map((role, index) => (
                      <li key={index}>{role}</li>
                    ))}
                  </ul>
                ) : (
                  <div>No roles assigned</div>
                )} */}
              </div>
            </Tab>
            <Tab
              eventKey="telemetry"
              title={
                <span>
                  <i className="bi bi-bar-chart-line" /> Telemetry
                </span>
              }
            >
               {/* <div className="p-3 border rounded" style={{ maxHeight: "60vh", overflowY: "auto" }}>
                <Table
                  responsive
                  hover
                  className="shadow-sm"
                  style={{ borderCollapse: "collapse" }}
                >
                  <thead className="bg-primary text-white">
                    <tr>
                      <th className="text-center" style={{ width: "5%" }}>#</th>
                      <th
                        className="text-center"
                        style={{ width: "10%", cursor: "pointer" }}
                      >
                        <i className="bi bi-arrow-right-circle-fill icon-margin-right" />
                        <span>Method</span>
                      </th>
                      <th
                        className="text-center"
                        style={{ width: "25%", cursor: "pointer" }}
                      >
                        <i className="bi bi-file-earmark-code-fill icon-margin-right" />
                        <span>Path</span>
                      </th>
                      <th 
                        className="text-center" 
                        style={{ width: "15%" }}
                      >
                        <i className="bi bi-patch-check-fill icon-margin-right" />
                        <span>Status Code</span>
                      </th>
                      <th 
                        className="text-center" 
                        style={{ width: "20%" }}
                      >
                        <i className="bi bi-speedometer2 icon-margin-right" />
                        <span>Response Time (ms)</span>
                      </th>
                      <th 
                        className="text-center" 
                        style={{ width: "25%" }}
                      >
                        <i className="bi bi-clock-history icon-margin-right" />
                        <span>Timestamp</span>
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {user.telemetry.map((telemetry, idx) => (
                      <tr key={telemetry.id}>
                        <td className="text-center">
                          {idx}
                        </td>
                        <td className="text-center">
                          <Button
                            variant="link"
                            className="text-decoration-none text-primary fw-bold"
                            onClick={() => navigate(`/telemetry/${telemetry.id}`)}
                          >
                            {telemetry.method}
                          </Button>
                        </td>
                        <td className="text-center">{telemetry.path}</td>
                        <td className="text-center">
                          <Badge bg="dark" pill>
                            {telemetry.statusCode}
                          </Badge>
                        </td>
                        <td className="text-center">
                          <Badge bg="info">{telemetry.responseTime} ms</Badge>
                        </td>
                        <td className="text-center">
                          <Badge bg="primary">{telemetry.requestTimestamp}</Badge>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </div> */}
            </Tab>
            <Tab
              eventKey="logins"
              title={
                <span>
                  <i className="bi bi-box-arrow-left" /> Logins
                </span>
              }
            >

            </Tab>
            <Tab
              eventKey="logouts"
              title={
                <span>
                  <i className="bi bi-box-arrow-right" /> Logouts
                </span>
              }
            >

            </Tab>
          </Tabs>
        </Col>
      </Row>

      <ConfirmModal
        show={showDeleteModal}
        onClose={handleCloseDeleteModal}
        onConfirm={handleConfirmDelete}
        title="Confirm Deletion"
        message="Are you sure you want to delete this user? This action cannot be undone."
      />

      {/* <Drawer
        roles={roles}
        show={showRoleDrawer}
        onClose={handleCloseRoleDrawer}
        handleAssignRoleToUser={handleAssignRoleToUser}
        handleUnassignRoleFromUser={handleUnassignRoleFromUser}
        userId={id!}
        currentRoles={user.roles}
      /> */}
    </div>
  );
};

export default View;
