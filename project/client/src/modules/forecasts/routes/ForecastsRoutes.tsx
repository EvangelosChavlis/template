// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';
const Statistics = lazy(() => import('src/modules/forecasts/pages/Statistics/Statistics'));
const Create = lazy(() => import('src/modules/forecasts/pages/Create/Create'));
const Initialize = lazy(() => import('src/modules/forecasts/pages/Initialize/Initialize'));
const Forecasts = lazy(() => import('src/modules/forecasts/pages/Table/Forecasts'));
const Update = lazy(() => import('src/modules/forecasts/pages/Update/Update'));
const View = lazy(() => import('src/modules/forecasts/pages/View/View'));

const ForecastsRoutes = () => (
  <Routes>
    <Route
      path="/forecasts"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading forecasts ..." />}>
              <Forecasts />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/forecasts/statistics"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading forecasts statistics ..." />}>
              <Statistics />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/forecasts/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading forecast details ..." />}>
              <View />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/forecasts/create"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading create forecast form ..." />}>
              <Create />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/forecasts/initialize"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading initialization ..." />}>
              <Initialize />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/forecasts/update/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading update forecast form ..." />}>
              <Update />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default ForecastsRoutes;
