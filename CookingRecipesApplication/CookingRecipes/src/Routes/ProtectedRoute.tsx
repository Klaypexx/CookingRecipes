import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { warnToast } from '../Components/Toast/Toast';
import { useEffect } from 'react';
import useAuthStore from '../Stores/useAuthStore';

function ProtectedRoute({ redirectPath = '/', children }: any) {
  const { isAuthorized } = useAuthStore();
  const location = useLocation();

  useEffect(() => {
    if (!isAuthorized) {
      warnToast('Вы не вошли в систему');
    }
  }, [isAuthorized]);

  if (!isAuthorized) {
    return <Navigate to={location.state?.from || redirectPath} replace />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
