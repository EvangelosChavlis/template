// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';
const Create = lazy(() => import('src/modules/roles/pages/Create/Create'));
const Initialize = lazy(() => import('src/modules/roles/pages/Initialize/Initialize'));
const Roles = lazy(() => import('src/modules/roles/pages/Table/Roles'));
const Update = lazy(() => import('src/modules/roles/pages/Update/Update'));
const View = lazy(() => import('src/modules/roles/pages/View/View'));

const RolesRoutes = () => (
  <Routes>
    <Route
      path="/roles"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading roles ..." />}>
              <Roles />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/roles/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading role details ..." />}>
              <View />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/roles/create"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading create role form ..." />}>
              <Create />
            </Suspense>
          }
        />
      }
    />
    <Route
      path="/roles/initialize"
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
      path="/roles/update/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading update role form ..." />}>
              <Update />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default RolesRoutes;
