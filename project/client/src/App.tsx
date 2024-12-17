// packages
import { BrowserRouter as Router } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ToastContainer } from 'react-toastify';

// source
import Menu from 'src/modules/shared/Menu';
import AppRoutes from 'src/routes/Routes';
import Footer from 'src/modules/shared/Footer';

// styles
import "bootstrap-icons/font/bootstrap-icons.css";
import 'react-toastify/dist/ReactToastify.css';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <div className="d-flex flex-column min-vh-100">
          <Menu />
          <AppRoutes />
          <Footer />
        </div>
      </Router>
      <ToastContainer />
    </QueryClientProvider>
  );
}

export default App;
