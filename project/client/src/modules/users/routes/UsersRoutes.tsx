// packages
import { Route, Routes } from 'react-router-dom';

// source
import Create from 'src/modules/users/pages/Create/Create';
import Initialize from 'src/modules/users/pages/Initialize/Initialize';
import Users from 'src/modules/users/pages/Table/Users';
import Update from 'src/modules/users/pages/Update/Update';
import View from 'src/modules/users/pages/View/View';

const UsersRoutes = () => (
  <Routes>
    <Route path="/users" element={<Users />} />
    <Route path="/users/:id" element={<View />} />
    <Route path="/users/create" element={<Create />} />
    <Route path="/users/initialize" element={<Initialize />} />
    <Route path="/users/update/:id" element={<Update />} />
  </Routes>
);

export default UsersRoutes;
