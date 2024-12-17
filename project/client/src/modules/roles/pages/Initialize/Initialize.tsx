// packages
import Form from "react-bootstrap/Form";
import Table from "react-bootstrap/Table";
import ListGroup from "react-bootstrap/ListGroup";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import useInitialize from "src/modules/roles/pages/Initialize/useInitialize";
import Header from "src/modules/shared/Header";

const Initialize = () => {
  const { formDataList, currentRole, handleChange, addForm, removeForm, submitRoles } = useInitialize();

  const header = "Initialize Roles";
  const buttons: ButtonProps[] = [
    {
      title: "Add Role",
      action: addForm,
      icon: <i className="bi bi-plus"></i>,
      color: "primary",
      placement: "top",
      disabled: false,
    },
    {
      title: "Submit",
      action: submitRoles,
      icon: <i className="bi bi-check"></i>,
      color: "success",
      placement: "top",
      disabled: formDataList.length === 0,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />

      <ListGroup className="mb-4">
        {formDataList.map((role, idx) => (
          <ListGroup.Item
            key={idx}
            className="p-3 border rounded bg-light m-2 d-flex flex-column"
            style={{ flex: 1, overflowY: "auto" }}
          >
            <div className="mt-1">
              <strong className="strong-margin-right">
                <i className="bi bi-chat icon-margin-right" /> Name
              </strong>
              <span>{role.name}</span>
            </div>
            <div className="mt-2">
              <strong className="strong-margin-right">
                <i className="bi bi-chat-dots icon-margin-right" /> Description
              </strong>
              <span>{role.description}</span>
            </div>
            <div className="d-flex justify-content-end">
              <i onClick={() => removeForm(idx)} className="bi bi-trash3-fill text-danger" />
            </div>
          </ListGroup.Item>
        ))}
      </ListGroup>

      <Form className="p-3 border rounded mt-2">
        <Table
          responsive
          hover
          className="shadow-sm"
          style={{
            borderCollapse: "collapse",
          }}
        >
          <thead>
            <tr>
              <th>Name</th>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>
                <Form.Control
                  name="name"
                  value={currentRole.name}
                  onChange={handleChange}
                  placeholder="Role name..."
                  aria-label="name"
                />
              </td>
              <td>
                <Form.Control
                  name="description"
                  value={currentRole.description}
                  onChange={handleChange}
                  as="textarea"
                  placeholder="Role description..."
                  aria-label="description"
                />
              </td>
            </tr>
          </tbody>
        </Table>
      </Form>
    </div>
  );
};

export default Initialize;
