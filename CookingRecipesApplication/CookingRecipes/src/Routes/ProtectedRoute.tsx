import { Navigate, Outlet, useLocation } from 'react-router-dom';
import useAuthStore from '../Stores/useAuthStore';

function ProtectedRoute({ redirectPath = '/', children }: any) {
  const { isAuthorized } = useAuthStore();
  const location = useLocation();

  if (!isAuthorized) {
    return <Navigate to={location.state?.from || redirectPath} replace />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
