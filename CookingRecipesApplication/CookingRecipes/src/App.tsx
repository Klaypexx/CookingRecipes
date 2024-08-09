/* eslint-disable @typescript-eslint/no-explicit-any */
import Header from './Components/Header/Header';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Footer from './Components/Footer/Footer';
import ModalBlock from './Components/Modal/ModalBlock/ModaBlock';

function App() {
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
