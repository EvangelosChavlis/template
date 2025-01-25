// packages
import { Navigate } from 'react-router-dom';

// source
import { getAuthToken } from 'src/utils/utils';

interface ProtectedRouteProps {
  element: JSX.Element;
  redirectTo: string;
}

const ProtectedRoute = ({ element, redirectTo }: ProtectedRouteProps) => {
  const token = getAuthToken();

  return token ? element : <Navigate to={redirectTo} />;
};


export default ProtectedRoute;
