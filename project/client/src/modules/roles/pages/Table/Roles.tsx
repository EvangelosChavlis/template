// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";

// source
import useRoles from "src/modules/roles/pages/Table/useRoles";
import Header from "src/modules/shared/Header";
import { ButtonProps } from "src/models/shared/buttonProps";
import { ItemRoleDto } from "src/models/auth/roleDto";
import LoadingSpinner from "src/modules/shared/LoadingSpinner";

const Roles = () => {
  const { roles } = useRoles();
  const navigate = useNavigate();

  const navigateClick = (roleId: string) => {
    navigate(`/roles/${roleId}`);
  };

  if (!roles) return <LoadingSpinner />;

  const header = "Roles Page";
  const buttons: ButtonProps[] = [
    {
      title: "Create Role",
      action: () => navigate("create"),
      icon: <i className="bi bi-plus-square"></i>,
      color: "primary",
      placement: "top",
      disabled: false,
    },
    {
      title: "Initialize Roles",
      action: () => navigate("initialize"),
      icon: <i className="bi bi-folder-plus"></i>,
      color: "success",
      placement: "top",
      disabled: false,
    },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        {roles && roles.length > 0 ? (
          <Table
            responsive
            hover
            className="shadow-sm"
            style={{
              borderCollapse: "collapse",
            }}>
            <thead>
              <tr>
                <th className="text-center" style={{ width: "5%" }}>#</th>
                <th className="text-center" style={{ width: "30%" }}>
                  <i className="bi bi-chat icon-margin-right"/>
                  <span>Name</span>
                </th>
                <th className="text-center" style={{ width: "40%" }}>
                  <i className="bi bi-chat-dots icon-margin-right"/>
                  <span>Description</span>
                </th>
                <th className="text-center" style={{ width: "15%" }}>
                  Active
                </th>
              </tr>
            </thead>
            <tbody>
              {roles.map((role: ItemRoleDto, idx) => (
                <tr key={role.id}>
                  <td className="text-center">{idx + 1}</td>
                  <td className="text-center">
                    <Button
                      variant="link"
                      className="text-decoration-none text-primary fw-bold"
                      onClick={() => navigateClick(role.id)}
                    >
                      {role.name}
                    </Button>
                  </td>
                  <td className="text-center">{role.description}</td>
                  <td className="text-center">
                    {role.isActive ? (
                      <i className="bi bi-toggle-on text-success"></i>
                    ) : (
                      <i className="bi bi-toggle-off text-danger"></i>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          <div className="alert alert-warning text-center">
            No roles found.
          </div>
        )}
      </div>
    </div>
  );
};

export default Roles;
