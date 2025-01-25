// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';
const Telemetry = lazy(() => import('src/modules/telemetry/pages/Table/Telemetry'));
const View = lazy(() => import('src/modules/telemetry/pages/View/View'));

const TelemetryRoutes = () => (
  <Routes>
    <Route
      path="/telemetry"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading telemetry ..." />}>
              <Telemetry />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/telemetry/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading telemetry details ..." />}>
              <View />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default TelemetryRoutes;
