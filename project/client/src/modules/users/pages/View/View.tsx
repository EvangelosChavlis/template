// packages
import { useState } from "react";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import Header from "src/modules/shared/Header";
import LoadingSpinner from "src/modules/shared/LoadingSpinner";
import useView from "src/modules/users/pages/View/useView";
import ConfirmModal from "src/modules/shared/ConfirmModal";

const View = () => {
  const {
    user,
    handleDelete,
    handleActivateUser,
    handleDeactivateUser,
    handleLockUser,
    handleUnlockUser,
    handleGeneratePasswordUser,
    navigate,
    id,
  } = useView();

  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const handleShowDeleteModal = () => setShowDeleteModal(true);
  const handleCloseDeleteModal = () => setShowDeleteModal(false);

  const handleConfirmDelete = () => {
    handleDelete();
    setShowDeleteModal(false);
  };

  if (!user) return <LoadingSpinner />;

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
      icon: <i className="bi bi-check-circle-fill"></i>,
      color: "info",
      placement: "top",
      disabled: user.isActive,
    },
    {
      title: "Deactivate User",
      action: () => handleDeactivateUser(),
      icon: <i className="bi bi-x-circle-fill"></i>,
      color: "info",
      placement: "top",
      disabled: !user.isActive,
    },
    {
      title: "Reset Password",
      action: () => handleGeneratePasswordUser(),
      icon: <i className="bi bi-arrow-clockwise"></i>,
      color: "info",
      placement: "top",
      disabled: false,
    },
  ];

  return (
    <div className="container mt-4 mb-3">
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
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-2">
                  <strong className="strong-margin-right">
                    <i className="bi bi-person-fill icon-margin-right" />First Name
                  </strong>
                  <span>{user.firstName}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-person icon-margin-right" />Last Name
                  </strong>
                  <span>{user.lastName}</span>
                </div>
                <hr />
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-envelope-fill icon-margin-right" /> Email
                  </strong>
                  <span>{user.email}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-envelope  icon-margin-right" />Email Confirmed
                  </strong>
                  {user.emailConfirmed ? (
                    <i className="bi bi-check-square-fill text-success" />
                  ) : (
                    <i className="bi bi-x-square-fill text-danger" />
                  )}
                </div>
                <hr />
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-person-badge-fill icon-margin-right"/> Username
                  </strong>
                  <span>{user.userName}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-key-fill icon-margin-right" />Initial Password
                  </strong>
                  <span>{user.initialPassword}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-person-fill-lock  icon-margin-right" /> Locked
                  </strong>
                  {user.lockoutEnabled ? (
                    <i className="bi bi-lock-fill text-danger" />
                  ) : (
                    <i className="bi bi-unlock-fill text-success" />
                  )}
                </div>
                <hr />
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-calendar-event-fill  icon-margin-right" /> Date of Birth
                  </strong>
                  <span>{user.dateOfBirth}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-file-earmark-text-fill icon-margin-right" /> Bio
                  </strong>
                  <span>{user.bio}</span>
                </div>
              </div>
            </Tab>
            <Tab
              eventKey="contact-info"
              title={
                <span>
                  <i className="bi bi-telephone-fill" /> Contact Info
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-2">
                  <strong className="strong-margin-right">
                    <i className="bi bi-geo-alt-fill icon-margin-right" /> Address
                  </strong>
                  <span>{user.address}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-postcard-fill icon-margin-right"/> Zip code
                  </strong>
                  <span>{user.zipCode}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-building-fill icon-margin-right" /> City
                  </strong>
                  <span>{user.city}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-map-fill icon-margin-right" /> State
                  </strong>
                  <span>{user.state}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-globe2 icon-margin-right" /> Country
                  </strong>
                  <span>{user.country}</span>
                </div>
                <hr />
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-telephone-fill icon-margin-right" /> Phone Number
                  </strong>
                  <span>{user.phoneNumber}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-telephone icon-margin-right" /> Phone Confirmed
                  </strong>
                  {user.phoneNumberConfirmed ? (
                    <i className="bi bi-check-square-fill text-success" />
                  ) : (
                    <i className="bi bi-x-square-fill text-danger" />
                  )}
                </div>
                <hr />
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-phone-fill icon-margin-right" /> Mobile Phone Number
                  </strong>
                  <span>{user.mobilePhoneNumber}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-phone icon-margin-right" /> Mobile Phone Confirmed
                  </strong>
                  {user.mobilePhoneNumberConfirmed ? (
                    <i className="bi bi-check-square-fill text-success" />
                  ) : (
                    <i className="bi bi-x-square-fill text-danger" />
                  )}
                </div>
              </div>
            </Tab>
            <Tab
              eventKey="system-info"
              title={
                <span>
                  <i className="bi bi-gear-fill" /> System Info
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-toggle-on icon-margin-right" /> Active
                  </strong>
                  {user.isActive ? (
                    <i className="bi bi-check-square-fill text-success" /> 
                  ) : (
                    <i className="bi bi-x-square-fill text-danger" />
                  )}
                </div>
              </div>
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
                {user.roles.length > 0 ? (
                  <ul>
                    {user.roles.map((role, index) => (
                      <li key={index}>{role}</li>
                    ))}
                  </ul>
                ) : (
                  <div>No roles assigned</div>
                )}
              </div>
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
    </div>
  );
};

export default View;
