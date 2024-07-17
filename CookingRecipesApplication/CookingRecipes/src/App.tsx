/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from 'react'
import './App.css'
import AuthService from './Services/AuthService'
import { useAuthStore } from './Stores/useAuthStore';


function App() {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const token = useAuthStore((state) => state.token);

  useEffect(() => {
    console.log(token);
  }, [token])

  const handleLogin = async (e: any) => {
    e.preventDefault();
    await AuthService.login(userName, password)
      .then((data) => console.log(data))
  }

  return (
    <>
      <h3>Login</h3>
      <div>
        <form onSubmit={handleLogin}>
          <br />
          <input
            type="username"
            placeholder="username"
            required
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />
          <br />
          <br />
          <input
            type="password"
            placeholder="password"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <br />
          <button type="submit">Signup</button>
        </form>
      </div>

      {/* <h3>Get User Data</h3>
      <div>
        <form onSubmit={getUserName}>
          <h3>Signup Form</h3>
          <br />
          <input
            type="username"
            placeholder="username"
            required
            value={username}
            onChange={(e) => setUserName(e.target.value)}
          />
          <br />
          <button type="submit">Signup</button>
        </form>
      </div> */}
    </>
  )
}

export default App
