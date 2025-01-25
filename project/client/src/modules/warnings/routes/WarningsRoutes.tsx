// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';
const Create = lazy(() => import('src/modules/warnings/pages/Create/Create'));
const Initialize = lazy(() => import('src/modules/warnings/pages/Initialize/Initialize'));
const Warnings = lazy(() => import('src/modules/warnings/pages/Table/Warnings'));
const Update = lazy(() => import('src/modules/warnings/pages/Update/Update'));
const View = lazy(() => import('src/modules/warnings/pages/View/View'));

const WarningsRoutes = () => (
  <Routes>
    <Route
      path="/warnings"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading warnings ..." />}>
              <Warnings />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/warnings/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading warning details ..." />}>
              <View />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/warnings/create"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading create warning form ..." />}>
              <Create />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/warnings/initialize"
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
      path="/warnings/update/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading update warning form ..." />}>
              <Update />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default WarningsRoutes;
