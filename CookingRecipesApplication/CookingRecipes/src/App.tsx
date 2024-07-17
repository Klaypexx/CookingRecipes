import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { loginAPI, registerAPI } from './Services/AuthService'
import { useEffect } from 'react';

function App() {

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");
    if (user && token) {
      setUser(JSON.parse(user));
      setToken(token);
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    }
    setIsReady(true);
  }, []);

  return (
    <>
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <p>Регистрация</p>
        <button onClick={() => registerAPI("Dmitri", 'Sanchezz', '12345')}>
        </button>
        <p>Логин</p>
        <button onClick={() => loginAPI('Sanchezz', '12345')}>
        </button>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
