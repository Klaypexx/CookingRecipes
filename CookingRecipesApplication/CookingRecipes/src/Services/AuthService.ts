import api from "../util/api";
import TokenService from "./TokenService";

const endpoints = {
    login: '/users/login',
    register: '/users/registers'
  };

const register = async (username: string, password: string) => {
  return await api.post(endpoints.register, {
    username,
    password
  });
};

const login = async (username: string, password: string) => {
  const response = await api
        .post(endpoints.login, {
            username,
            password
        });
    if (response.data) {
        TokenService.setToken(response.data);
    }
    return response;
};

// const logout = () => {
//   TokenService.removeUser();
// };

// const getCurrentUser = () => {
//   return JSON.parse(localStorage.getItem("user"));
// };

const AuthService = {
  register,
  login
};

export default AuthService;