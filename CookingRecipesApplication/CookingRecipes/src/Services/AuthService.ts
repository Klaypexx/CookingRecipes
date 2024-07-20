import api from "../util/api";
import TokenService from "./TokenService";

const endpoints = {
    login: '/users/login',
    register: '/users/registers',
    refresh: '/users/refresh'
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

const refresh = async () => {
  // eslint-disable-next-line react-hooks/rules-of-hooks
  const response = await api.post(endpoints.refresh);
  if (response.data) {
    TokenService.setToken(response.data);
  }
  return response;
}

const checkAuth = async () => {
  const response = await AuthService.refresh()
  return response;
}

const logout = () => {
  TokenService.removeToken();
};

// const getCurrentUser = () => {
//   return JSON.parse(localStorage.getItem("user"));
// };

const AuthService = {
  register,
  login,
  refresh,
  checkAuth,
  logout
};

export default AuthService;