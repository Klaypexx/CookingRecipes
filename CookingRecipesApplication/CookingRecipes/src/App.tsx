/* eslint-disable @typescript-eslint/no-explicit-any */
import Header from './Components/Header/Header';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Footer from './Components/Footer/Footer';
import ModalBlock from './Components/Modal/ModalBlock/ModaBlock';
import { Suspense } from 'react';
import Spinner from './Components/Spinner/Spinner';

function App() {
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
