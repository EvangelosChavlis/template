import { BrowserRouter as Router, useLocation } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ToastContainer } from 'react-toastify';
import { lazy, Suspense } from 'react';

// source
import Menu from 'src/modules/shared/Menu';
import Footer from 'src/modules/shared/Footer';
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';
const AppRoutes = lazy(() => import('src/routes/Routes'));

// styles
import "bootstrap-icons/font/bootstrap-icons.css";
import 'react-toastify/dist/ReactToastify.css';

const queryClient = new QueryClient();

const LayoutWrapper = () => {
  const location = useLocation();

  // Define routes where Menu and Footer are hidden
  const hideLayoutRoutes = ['/auth/login', '/auth/register'];

  const shouldHideLayout = hideLayoutRoutes.includes(location.pathname);

  return (
    <div className="d-flex flex-column min-vh-100">
      {!shouldHideLayout && <Menu />}
      <Suspense fallback={<LoadingSpinner message="Loading application ..." />}>
        <AppRoutes />
      </Suspense>
      {!shouldHideLayout && <Footer />}
    </div>
  );
}

const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <LayoutWrapper />
      </Router>
      <ToastContainer />
    </QueryClientProvider>
  );
}

export default App;
