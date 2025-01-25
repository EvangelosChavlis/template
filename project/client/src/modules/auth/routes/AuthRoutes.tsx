// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
const Register = lazy(() => import('src/modules/auth/pages/Register/Register'));
const Login = lazy(() => import('src/modules/auth/pages/Login/Login'));

const AuthRoutes = () => (
  <Routes>
    <Route
      path="/auth/login"
      element={
        <Suspense fallback={<LoadingSpinner message="Loading auth ..." />}>
          <Login />
        </Suspense>
      }
    />
    <Route
      path="/auth/register"
      element={
        <Suspense fallback={<LoadingSpinner message="Loading register ..." />}>
          <Register />
        </Suspense>
      }
    />
  </Routes>
);

export default AuthRoutes;
