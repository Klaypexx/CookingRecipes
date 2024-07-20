/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  Navigate,
  Outlet,
  useLocation,
} from 'react-router-dom';
import { useAuthStore } from '../Stores/useAuthStore';

function ProtectedRoute({
  redirectPath = '/',
  children,
}: any) {
  const token = useAuthStore((state) => state.token);
  const location = useLocation();

  if (!token) {
    return <Navigate to={redirectPath} replace state={{ from: location }} />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
