import { AxiosError } from 'axios';
import api from '../util/api';
import TokenService from './TokenService';

const endpoints = {
  login: '/users/login',
  register: '/users/register',
  refresh: '/users/refresh',
  logout: '/users/logout',
};

const register = async (name: string, username: string, password: string) => {
  try {
    const response = await api.post(endpoints.register, {
      name,
      username,
      password,
    });
    return { success: true, response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { success: false, message: error.response?.data?.message || 'Произошла ошибка при входе' };
    }
    return { success: false, message: 'Произошла неизвестная ошибка при входе' };
  }
};

const login = async (username: string, password: string) => {
  try {
    const response = await api.post(endpoints.login, {
      username,
      password,
    });
    if (response.data) {
      TokenService.setToken(response.data);
    }
    return { success: true, response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { success: false, message: error.response?.data?.message || 'Произошла ошибка при входе' };
    }
    return { success: false, message: 'Произошла неизвестная ошибка при входе' };
  }
};

const refresh = async () => {
  const response = await api.post(endpoints.refresh);
  if (response.data) {
    TokenService.setToken(response.data);
  }
  return response;
};

const checkAuth = async () => {
  const response = await AuthService.refresh();
  return response;
};

const logout = async () => {
  const response = await api.post(endpoints.logout);
  TokenService.removeToken();
  return response;
};

const AuthService = {
  register,
  login,
  refresh,
  checkAuth,
  logout,
};

export default AuthService;
