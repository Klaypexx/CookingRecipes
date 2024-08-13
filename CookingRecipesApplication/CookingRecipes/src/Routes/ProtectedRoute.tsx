import { Navigate, Outlet } from 'react-router-dom';
import TokenService from '../Services/TokenService';
import { warnToast } from '../Components/Toast/Toast';
import { useEffect } from 'react';
import useModalStore from '../Stores/useModalStore';

function ProtectedRoute({ redirectPath = '/', children }: any) {
  const { isAuth, setAuth } = useModalStore();
  const token = TokenService.getAccessToken();

  useEffect(() => {
    if (!token) {
      warnToast('Вы не вошли в систему');
      setAuth(isAuth);
    }
  }, [token]);

  if (!token) {
    return <Navigate to={redirectPath} replace />;
  }

  return children || <Outlet />;
}

export default ProtectedRoute;
