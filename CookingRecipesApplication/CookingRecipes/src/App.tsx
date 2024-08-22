/* eslint-disable @typescript-eslint/no-explicit-any */
import Header from './Components/Header/Header';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Footer from './Components/Footer/Footer';
import ModalBlock from './Components/Modal/ModalBlock/ModaBlock';
import { Suspense, useEffect } from 'react';
import Spinner from './Components/Spinner/Spinner';
import TokenService from './Services/TokenService';
import AuthService from './Services/AuthService';
import useAuthStore from './Stores/useAuthStore';

function App() {
  const token = TokenService.getAccessToken();
  const { setAuthorized } = useAuthStore();

  useEffect(() => {
    console.log('Перезагрузка');
    if (token) {
      const fetchAuth = async () => {
        try {
          await AuthService.isAuth();
          setAuthorized(true);
        } catch (error) {
          setAuthorized(false);
        }
      };
      fetchAuth();
    }
  }, []);

  return (
    <>
      <Header />
      <Suspense fallback={<Spinner />}>
        <Outlet />
      </Suspense>
      <Footer />
      <ModalBlock />
      <ToastContainer />
    </>
  );
}

export default App;
