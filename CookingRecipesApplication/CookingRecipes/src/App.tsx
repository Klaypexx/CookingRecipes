/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Footer from './Components/Footer/Footer';
import Header from './Components/Header/Header';
import ModalBlock from './Components/Modal/ModalBlock/ModaBlock';
import Spinner from './Components/Spinner/Spinner';
import AuthService from './Services/AuthService';
import TokenService from './Services/TokenService';
import UserService from './Services/UserService';
import useAuthStore from './Stores/useAuthStore';
import useUserStore from './Stores/useUserStore';

function App() {
  const token = TokenService.getAccessToken();
  const { setAuthorized, isAuthorized } = useAuthStore();
  const [loading, setLoading] = useState(true);
  const { setUserName } = useUserStore();

  useEffect(() => {
    const fetchAuth = async () => {
      if (token) {
        await AuthService.isAuth()
          .then(() => {
            setAuthorized(true);
          })
          .catch(() => {
            setAuthorized(false);
          });
        await UserService.username().then((res) => {
          if (res) {
            setUserName(res.response.data.userName);
          }
        });
      }
      setLoading(false);
    };
    fetchAuth();
  }, [isAuthorized]);

  useEffect(() => {
    if (isAuthorized) {
      const interval = setInterval(async () => {
        await AuthService.refresh();
      }, 150000);

      return () => clearInterval(interval);
    }
  }, [isAuthorized]);

  if (loading) {
    return <Spinner />;
  }

  return (
    <>
      <Header />
      <Outlet />
      <ModalBlock />
      <ToastContainer />
      <Footer />
    </>
  );
}

export default App;
