import { Route, Routes } from 'react-router-dom';

import Create from 'src/modules/warnings/pages/Create/Create';
import Initialize from 'src/modules/warnings/pages/Initialize/Initialize';
import Warnings from 'src/modules/warnings/pages/Table/Warnings';
import Update from 'src/modules/warnings/pages/Update/Update';
import View from 'src/modules/warnings/pages/View/View';

const WarningsRoutes = () => (
  <Routes>
    <Route path="/warnings" element={<Warnings />} />
    <Route path="/warnings/:id" element={<View />} />
    <Route path="/warnings/create" element={<Create />} />
    <Route path="/warnings/initialize" element={<Initialize />} />
    <Route path="/warnings/update/:id" element={<Update />} />
  </Routes>
);

export default WarningsRoutes;
