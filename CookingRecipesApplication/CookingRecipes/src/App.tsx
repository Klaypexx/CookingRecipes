/* eslint-disable @typescript-eslint/no-explicit-any */
import './App.css'
import Header from './Components/Header/Header';
import { Outlet } from 'react-router-dom';


function App() {


  return (
    <>
      <Header />
      <Outlet />
    </>
  )
}

export default App
