// packages
import { Route, Routes } from 'react-router-dom';

// source
import Create from 'src/modules/forecasts/pages/Create/Create';
import Initialize from 'src/modules/forecasts/pages/Initialize/Initialize';
import Forecasts from 'src/modules/forecasts/pages/Table/Forecasts';
import Update from 'src/modules/forecasts/pages/Update/Update';
import View from 'src/modules/forecasts/pages/View/View';

const ForecastsRoutes = () => (
  <Routes>
    <Route path="/forecasts" element={<Forecasts />} />
    <Route path="/forecasts/:id" element={<View />} />
    <Route path="/forecasts/create" element={<Create />} />
    <Route path="/forecasts/initialize" element={<Initialize />} />
    <Route path="/forecasts/update/:id" element={<Update />} />
  </Routes>
);

export default ForecastsRoutes;
