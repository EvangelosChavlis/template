// packages
import { Fragment } from "react";
import { Badge } from "react-bootstrap";
import { useState } from "react";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import Header from "src/modules/shared/Header";
import useView from "src/modules/warnings/pages/View/useView";
import ConfirmModal from "src/modules/shared/ConfirmModal";

const View = () => {
  const {
    warning,
    handleDelete,
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
  

  const header = "Warning Info";
  const buttons: ButtonProps[] = [
    {
      title: "Update Warning",
      action: () => navigate(`/warnings/update/${id}`),
      icon: <i className="bi bi-pencil-square" />,
      color: "warning",
      placement: "top",
      disabled: false,
    },
    {
      title: "Delete Warning",
      action: handleShowDeleteModal,
      icon: <i className="bi bi-trash3" />,
      color: "danger",
      placement: "top",
      disabled: false,
    }
  ];

  return (
    <div className="container mt-4 mb-4">
      <Header header={header} buttons={buttons} />
      <div
        className="p-3 border rounded bg-light mt-2"
        style={{ flex: 1, overflowY: "auto" }}
      >
        <div className="mt-2">
          <strong className="strong-margin-right">
            <i className="bi bi-chat icon-margin-right"/> Name
          </strong>
          <span>{warning.name}</span>
        </div>
        <div className="mt-5">
          <strong className="strong-margin-right">
            <i className="bi bi-chat-dots icon-margin-right" /> Description
          </strong>
          <span>{warning.description}</span>
        </div>
        {warning.forecasts.length > 0 && (
          <Fragment>
            <hr />
            <div className="mt-4">
              <strong className="strong-margin-right">
                <i className="bi bi-cloud-haze-fill icon-margin-right" /> Forecasts
              </strong>
              <ol className="mt-2">
                {warning.forecasts.map((forecast, index) => (
                  <li className="mt-3"  key={index}>
                    <Badge bg="dark">
                      {forecast} °C
                    </Badge> 
                  </li>
                ))}
              </ol>
            </div>
          </Fragment>
        )}
      </div>

      <ConfirmModal
        show={showDeleteModal}
        onClose={handleCloseDeleteModal}
        onConfirm={handleConfirmDelete}
        title="Confirm Deletion"
        message="Are you sure you want to delete this warning? This action cannot be undone."
      />
    </div>
  );
};

export default View;
