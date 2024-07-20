/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect } from 'react';
import Header from './Components/Header/Header';
import { Outlet, useNavigate } from 'react-router-dom';
import { navigation } from './util/api';

function App() {
  const navigate = useNavigate();

  useEffect(() => {
    navigation(navigate); // Передаем navigate в интерсепторы
  }, [navigate]);

  return (
    <>
      <Header />
      <Outlet />
    </>
  );
}

export default App;