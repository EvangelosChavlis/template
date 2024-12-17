// packages
import { useState } from "react";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import useView from "src/modules/roles/pages/View/useView";
import Header from "src/modules/shared/Header";
import LoadingSpinner from "src/modules/shared/LoadingSpinner";
import ConfirmModal from "src/modules/shared/ConfirmModal";

const View = () => {
  const {
    role,
    handleDelete,
    handleActivateRole,
    handleDeactivateRole,
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

  if (!role) return <LoadingSpinner />;

  const header = "Role Info";
  const buttons: ButtonProps[] = [
    {
      title: "Update Role",
      action: () => navigate(`/roles/update/${id}`),
      icon: <i className="bi bi-pencil-square" />,
      color: "warning",
      placement: "top",
      disabled: false
    },
    {
      title: "Delete Role",
      action: handleShowDeleteModal,
      icon: <i className="bi bi-trash3" />,
      color: "danger",
      placement: "top",
      disabled: false
    },
    {
      title: "Activate Role",
      action: () => handleActivateRole(),
      icon: <i className="bi bi-check-circle-fill" />,
      color: "info",
      placement: "top",
      disabled: role.isActive,
    },
    {
      title: "Deactivate Role",
      action: () => handleDeactivateRole(),
      icon: <i className="bi bi-x-circle-fill" />,
      color: "info",
      placement: "top",
      disabled: !role.isActive,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div
        className="p-3 border rounded bg-light mt-2"
        style={{ flex: 1, overflowY: "auto" }}
      >
        <div className="mt-2">
          <strong className="strong-margin-right">
            <i className="bi bi-chat icon-margin-right"/>Name
          </strong>
          <span>{role.name}</span>
        </div>
        <div className="mt-5">
          <strong className="strong-margin-right">
            <i className="bi bi-chat-dots icon-margin-right"/>Description
          </strong>
          <span>{role.description}</span>
        </div>
        <hr />
        <div className="mt-4">
          <strong className="strong-margin-right">
            <i className="bi bi-toggle-on icon-margin-right"/>Active
          </strong>
          {role.isActive ? (
            <i className="bi bi-check-square-fill text-success"/>
          ) : (
            <i className="bi bi-x-square-fill text-danger"/>
          )}
        </div>
      </div>
      
      <ConfirmModal
        show={showDeleteModal}
        onClose={handleCloseDeleteModal}
        onConfirm={handleConfirmDelete}
        title="Confirm Deletion"
        message="Are you sure you want to delete this role? This action cannot be undone."
      />
    </div>
  );
};

export default View;
