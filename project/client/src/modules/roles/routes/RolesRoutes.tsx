// packages
import { Route, Routes } from 'react-router-dom';

// source
import Create from 'src/modules/roles/pages/Create/Create';
import Initialize from 'src/modules/roles/pages/Initialize/Initialize';
import Roles from 'src/modules/roles/pages/Table/Roles';
import Update from 'src/modules/roles/pages/Update/Update';
import View from 'src/modules/roles/pages/View/View';

const RolesRoutes = () => (
  <Routes>
    <Route path="/roles" element={<Roles />} />
    <Route path="/roles/:id" element={<View />} />
    <Route path="/roles/create" element={<Create />} />
    <Route path="/roles/initialize" element={<Initialize />} />
    <Route path="/roles/update/:id" element={<Update />} />
  </Routes>
);

export default RolesRoutes;
