// packages
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
const AuthRoutes = lazy(() => import('src/modules/auth/routes/AuthRoutes'));
const HomeRoutes = lazy(() => import('src/modules/home/routes/HomeRoutes'));
const UsersRoutes = lazy(() => import('src/modules/users/routes/UsersRoutes'));
const RolesRoutes = lazy(() => import('src/modules/roles/routes/RolesRoutes'));
const ForecastsRoutes = lazy(() => import('src/modules/forecasts/routes/ForecastsRoutes'));
const WarningsRoutes = lazy(() => import('src/modules/warnings/routes/WarningsRoutes'));
const ErrorsRoutes = lazy(() => import('src/modules/errors/routes/ErrorsRoutes'));
const TelemetryRoutes = lazy(() => import('src/modules/telemetry/routes/TelemetryRoutes'));

const AppRoutes = () => (
  <Suspense fallback={<LoadingSpinner message="Loading routes application ..." />}>
    <AuthRoutes />
    <HomeRoutes />
    <UsersRoutes />
    <RolesRoutes />
    <ForecastsRoutes />
    <WarningsRoutes />
    <ErrorsRoutes />
    <TelemetryRoutes />
  </Suspense>
);

export default AppRoutes;
