// packages
import { useNavigate } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import Badge from "react-bootstrap/Badge";

// source
import { ButtonProps } from "src/models/shared/buttonProps";
import Header from "src/modules/shared/Header";
import TableFooter from "src/modules/shared/TableFooter";
import useUsers from "src/modules/users/pages/Table/useUsers";
import { incNumberFunction } from "src/utils/utils";

const Users = () => {
  const navigate = useNavigate();

  const { users, pagination, handlePageChange } = useUsers();

  const navigateClick = (userId: string) => {
    navigate(`/users/${userId}`);
  };

  const header = "Users Page";
  const buttons: ButtonProps[] = [
    {
      title: "Add User",
      action: () => navigate("create"),
      icon: <i className="bi bi-plus-square"></i>,
      color: "primary",
      placement: "top",
      disabled: false,
    },
    // {
    //   title: "Initialize Users",
    //   action: () => navigate("initialize"),
    //   icon: <i className="bi bi-folder-plus"></i>,
    //   color: "success",
    //   placement: "top",
    //   disabled: false,
    // },
  ];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        {users.length > 0 ? (
          <Table
            responsive
            hover
            className="shadow-sm"
            style={{
              borderCollapse: "collapse",
            }}
          >
            <thead className="bg-primary text-white">
              <tr>
                <th className="text-center" style={{ width: "5%", borderBottom: "2px solid #dee2e6" }}>#</th>
                <th className="text-center" style={{ width: "20%", borderBottom: "2px solid #dee2e6" }}>
                  <i className="bi bi-person-badge-fill icon-margin-right"/>
                  <span>Username</span>
                </th>
                <th className="text-center" style={{ width: "15%", borderBottom: "2px solid #dee2e6" }}>
                <i className="bi bi-person-fill icon-margin-right" />
                  <span>First Name</span>
                </th>
                <th className="text-center" style={{ width: "15%", borderBottom: "2px solid #dee2e6" }}>
                  <i className="bi bi-person icon-margin-right" />
                  <span>Last Name</span>
                </th>
                <th className="text-center" style={{ width: "25%", borderBottom: "2px solid #dee2e6" }}>
                  <i className="bi bi-envelope-fill icon-margin-right" />
                  <span>Email</span>
                </th>
                <th className="text-center" style={{ width: "10%", borderBottom: "2px solid #dee2e6" }}>
                  <i className="bi bi-telephone-fill icon-margin-right" />
                  <span>Phone</span>
                </th>
                <th className="text-center" style={{ width: "10%", borderBottom: "2px solid #dee2e6" }}>
                  <i className="bi bi-phone-fill icon-margin-right" />
                  <span>Mobile</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {users.map((user, idx) => (
                <tr key={user.id}>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    <Button
                      variant="link"
                      className="text-decoration-none text-primary fw-bold"
                      onClick={() => navigateClick(user.id)}
                    >
                      {user.userName}
                    </Button>
                  </td>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    {user.firstName}
                  </td>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    {user.lastName}
                  </td>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    {user.email}
                  </td>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    <Badge bg="info" text="dark">{user.phoneNumber}</Badge>
                  </td>
                  <td className="text-center" style={{ borderBottom: "1px solid #dee2e6" }}>
                    <Badge bg="secondary">{user.mobilePhoneNumber}</Badge>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          <div className="alert alert-warning text-center">
            No users found.
          </div>
        )}
        <TableFooter pagination={pagination} handlePageChange={handlePageChange} />
      </div>
    </div>
  );
};

export default Users;
