/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  Navigate,
  Outlet,
  useLocation,
} from 'react-router-dom';
import TokenService from '../Services/TokenService';

function ProtectedRoute({
  redirectPath = '/',
  children,
}: any) {
  const token = TokenService.getAccessToken();
  const location = useLocation();

  if (!token) {
    return <Navigate to={redirectPath} replace state={{ from: location }} />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
