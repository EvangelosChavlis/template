// source
import ErrorsRoutes from 'src/modules/errors/routes/ErrorsRoutes';
import ForecastsRoutes from 'src/modules/forecasts/routes/ForecastsRoutes';
import HomeRoutes from 'src/modules/home/routes/HomeRoutes';
import RolesRoutes from 'src/modules/roles/routes/RolesRoutes';
import TelemetryRoutes from 'src/modules/telemetry/routes/TelemetryRoutes';
import UsersRoutes from 'src/modules/users/routes/UsersRoutes';
import WarningsRoutes from 'src/modules/warnings/routes/WarningsRoutes';

const AppRoutes = () => (
  <>
    <HomeRoutes />
    <UsersRoutes />
    <RolesRoutes />
    <ForecastsRoutes />
    <WarningsRoutes />
    <ErrorsRoutes />
    <TelemetryRoutes /> 
  </>
);

export default AppRoutes;
