/* eslint-disable @typescript-eslint/no-explicit-any */
import Header from './Components/Header/Header';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Footer from './Components/Footer/Footer';
import ModalBlock from './Components/Modal/ModalBlock/ModaBlock';
import { Suspense, useEffect, useState } from 'react';
import Spinner from './Components/Spinner/Spinner';
import TokenService from './Services/TokenService';
import AuthService from './Services/AuthService';
import useAuthStore from './Stores/useAuthStore';

function App() {
  const token = TokenService.getAccessToken();
  const { setAuthorized, isAuthorized } = useAuthStore();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    console.log('Запрос');
    const fetchAuth = async () => {
      if (token) {
        await AuthService.isAuth()
          .then(() => {
            setAuthorized(true);
          })
          .catch(() => {
            setAuthorized(false);
          });
      }
      setLoading(false);
    };
    fetchAuth();
  }, []);

  useEffect(() => {
    if (isAuthorized) {
      const interval = setInterval(async () => {
        await AuthService.refresh().catch(async () => {
          setAuthorized(false);
          await AuthService.logout();
        });
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
      <Suspense fallback={<Spinner />}>
        <Outlet />
      </Suspense>
      <ModalBlock />
      <ToastContainer />
      <Footer />
    </>
  );
}

export default App;
