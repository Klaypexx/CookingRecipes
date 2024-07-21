/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  Navigate,
  Outlet,
  useLocation,
} from 'react-router-dom';
import TokenService from '../Services/TokenService';
import { warnToast } from '../Components/Toast/Toast';
import { useEffect } from 'react';

function ProtectedRoute({
  redirectPath = '/',
  children,
}: any) {
  const token = TokenService.getAccessToken();
  const location = useLocation();

  useEffect(() => {
    if (!token) {
      warnToast("Вы не вошли в систему");
    }
  }, [token]);

  if (!token) {
    // warnToast("Вы не вошли в систему")
    return <Navigate to={redirectPath} replace state={{ from: location }} />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
