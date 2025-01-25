// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';
const Errors = lazy(() => import('src/modules/errors/pages/Table/Errors'));
const View = lazy(() => import('src/modules/errors/pages/View/View'));

const ErrorsRoutes = () => (
  <Routes>
    <Route
      path="/errors"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading errors ..." />}>
              <Errors />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/errors/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading error details ..." />}>
              <View />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default ErrorsRoutes;
