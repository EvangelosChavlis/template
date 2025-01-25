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
import SortIcon from "src/modules/shared/SortIcon";

const Users = () => {
  const navigate = useNavigate();

  const { 
    users, 
    pagination, 
    handlePageChange, 
    handleRowsPerPageChange,
    handleFilterChange,
    handleSortByChange,
    sortBy,
    sortOrder,
    filter
  } = useUsers();

  const navigateClick = (userId: string) => {
    navigate(`/users/${userId}`);
  };

  const header = "Users Page";
  const buttons: ButtonProps[] = [
    {
      title: "Add User",
      action: () => navigate("create"),
      icon: <i className="bi bi-plus-square" />,
      color: "primary",
      placement: "top",
      disabled: false,
    },
  ];

  return (
    <div className="container mt-4 mb-4">
      <Header header={header} buttons={buttons} />
      <div className="p-3 border rounded mt-2">
        <div className="mb-3">
          <input
            type="text"
            value={filter}
            onChange={handleFilterChange}
            placeholder="Search here ..."
            className="form-control"
          />
        </div>
        
        <div style={{ maxHeight: '60vh', overflowY: 'auto' }}>
          <Table responsive hover className="shadow-sm" style={{ borderCollapse: "collapse" }}>
            <thead className="bg-primary text-white">
              <tr>
                <th className="text-center" style={{ width: "5%" }}>
                  #
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "20%", cursor: "pointer" }} 
                  onClick={() => handleSortByChange("UserName")}
                >
                  <SortIcon 
                    column="UserName" 
                    sortBy={sortBy} 
                    sortOrder={sortOrder} 
                  />
                  <i className="bi bi-person-badge-fill icon-margin-right" />
                  <span>Username</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "15%", cursor: "pointer" }} 
                  onClick={() => handleSortByChange("FirstName")}
                >
                  <SortIcon 
                    column="FirstName" 
                    sortBy={sortBy} 
                    sortOrder={sortOrder} 
                  />
                  <i className="bi bi-person-fill icon-margin-right" />
                  <span>First Name</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "15%", cursor: "pointer" }} 
                  onClick={() => handleSortByChange("LastName")}
                >
                  <SortIcon 
                    column="LastName" 
                    sortBy={sortBy} 
                    sortOrder={sortOrder} 
                  />
                  <i className="bi bi-person icon-margin-right" />
                  <span>Last Name</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "25%", cursor: "pointer" }} 
                  onClick={() => handleSortByChange("Email")}
                >
                  <SortIcon 
                    column="Email" 
                    sortBy={sortBy} 
                    sortOrder={sortOrder} 
                  />
                  <i className="bi bi-envelope-fill icon-margin-right" />
                  <span>Email</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "10%", cursor: "pointer" }} 
                  onClick={() => handleSortByChange("PhoneNumber")}
                >
                  <SortIcon 
                    column="PhoneNumber" 
                    sortBy={sortBy} 
                    sortOrder={sortOrder} 
                  />
                  <i className="bi bi-telephone-fill icon-margin-right" />
                  <span>Phone</span>
                </th>
                <th 
                  className="text-center" 
                  style={{ width: "10%", cursor: "pointer" }} 
                  onClick={() => handleSortByChange("MobilePhoneNumber")}
                >
                  <SortIcon 
                    column="MobilePhoneNumber" 
                    sortBy={sortBy} 
                    sortOrder={sortOrder} 
                  />
                  <i className="bi bi-phone-fill icon-margin-right" />
                  <span>Mobile</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {users.map((user, idx) => (
                <tr key={user.id}>
                  <td className="text-center">
                    {incNumberFunction(idx, pagination)}
                  </td>
                  <td className="text-center">
                    <Button
                      variant="link"
                      className="text-decoration-none text-primary fw-bold"
                      onClick={() => navigateClick(user.id)}
                    >
                      {user.userName}
                    </Button>
                  </td>
                  <td className="text-center">{user.firstName}</td>
                  <td className="text-center">{user.lastName}</td>
                  <td className="text-center">{user.email}</td>
                  <td className="text-center">
                    <Badge bg="info" text="dark">{user.phoneNumber}</Badge>
                  </td>
                  <td className="text-center">
                    <Badge bg="secondary">{user.mobilePhoneNumber}</Badge>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </div>
        <TableFooter
          pagination={pagination}
          handlePageChange={handlePageChange}
          rowsPerPage={pagination.pageSize}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </div>
    </div>
  );
};

export default Users;