/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect } from 'react';
import Header from './Components/Header/Header';
import { Outlet, useNavigate } from 'react-router-dom';
import { navigation } from './util/api';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Footer from './Components/Footer/Footer';
import ModalBlock from './Components/Modal/ModalBlock/ModaBlock';

function App() {
  const navigate = useNavigate();

  useEffect(() => {
    navigation(navigate); // Передаем navigate в интерсепторы
  }, [navigate]);

  return (
    <>
      <Header />
      <Outlet />
      <Footer />
      <ModalBlock />
      <ToastContainer />
    </>
  );
}

export default App;
