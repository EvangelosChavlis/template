// src/modules/users/routes/UsersRoutes.tsx
import { Routes, Route } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';

const Create = lazy(() => import('src/modules/users/pages/Create/Create'));
const Initialize = lazy(() => import('src/modules/users/pages/Initialize/Initialize'));
const Users = lazy(() => import('src/modules/users/pages/Table/Users'));
const Update = lazy(() => import('src/modules/users/pages/Update/Update'));
const View = lazy(() => import('src/modules/users/pages/View/View'));

const UsersRoutes = () => (
  <Routes>
    <Route
      path="/users"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading users ..." />}>
              <Users />
            </Suspense>
          }
        />
      }
    />
    
    <Route
      path="/users/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading user details ..." />}>
              <View />
            </Suspense>
          }
        />
      }
    />

    <Route
      path="/users/create"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading create user form ..." />}>
              <Create />
            </Suspense>
          }
        />
      }
    />

    <Route
      path="/users/initialize"
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
      path="/users/update/:id"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading update user form ..." />}>
              <Update />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default UsersRoutes;
