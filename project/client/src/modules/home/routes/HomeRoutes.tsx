// packages
import { Route, Routes } from 'react-router-dom';
import { lazy, Suspense } from 'react';

// source
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
import ProtectedRoute from 'src/modules/auth/routes/ProtectedRoute';

const Home = lazy(() => import('src/modules/home/pages/Home'));

const HomeRoutes = () => (
  <Routes>
    <Route
      path="/"
      element={
        <ProtectedRoute 
          redirectTo="/auth/login" 
          element={
            <Suspense fallback={<LoadingSpinner message="Loading home ..." />}>
              <Home />
            </Suspense>
          }
        />
      }
    />
  </Routes>
);

export default HomeRoutes;
