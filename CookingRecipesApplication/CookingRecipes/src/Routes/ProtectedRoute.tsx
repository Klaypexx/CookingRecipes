import { Navigate, Outlet, useLocation } from 'react-router-dom';
import TokenService from '../Services/TokenService';
import { warnToast } from '../Components/Toast/Toast';
import { useEffect } from 'react';
import useModalStore from '../Stores/useModalStore';
import useAuthStore from '../Stores/useAuthStore';

function ProtectedRoute({ redirectPath = '/', children }: any) {
  const { isAuthorized, setAuthorized } = useAuthStore();
  const { isAuth, setAuth, setFromPath } = useModalStore();
  const token = TokenService.getAccessToken();
  const location = useLocation();

  useEffect(() => {
    if (!isAuthorized) {
      if (!token) {
        warnToast('Вы не вошли в систему');
        setFromPath(location.pathname);
        setAuth(isAuth);
      } else {
        setAuthorized(true);
      }
    } else {
      setAuthorized(false);
    }
  }, [token]);

  if (!token) {
    return <Navigate to={redirectPath} replace />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
