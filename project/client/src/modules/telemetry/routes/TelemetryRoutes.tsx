// packages
import { Route, Routes } from 'react-router-dom';

// source
import Telemetry from 'src/modules/telemetry/pages/Table/Telemetry';
import View from 'src/modules/telemetry/pages/View/View';

const TelemetryRoutes = () => (
  <Routes>
    <Route path="/telemetry" element={<Telemetry />} />
    <Route path="/telemetry/:id" element={<View />} />
  </Routes>
);

export default TelemetryRoutes;
