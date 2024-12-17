// packages
import { Route, Routes } from 'react-router-dom';

// source
import Errors from 'src/modules/errors/pages/Table/Errors';
import View from 'src/modules/errors/pages/View/View';

const ErrorsRoutes = () => (
  <Routes>
    <Route path="/errors" element={<Errors />} />
    <Route path="/errors/:id" element={<View />} />
  </Routes>
);

export default ErrorsRoutes;
