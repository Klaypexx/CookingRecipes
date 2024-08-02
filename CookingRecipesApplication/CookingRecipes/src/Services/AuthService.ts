import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';
import TokenService from './TokenService';
import RegisterValues from '../Types/RegisterValues';
import LoginValues from '../Types/LoginValues';

const endpoints = {
  login: '/users/login',
  register: '/users/register',
  refresh: '/users/refresh',
  logout: '/users/logout',
};

const register = async (values: RegisterValues) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(endpoints.register, values);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.message || 'Произошла ошибка при входе' };
    }
    return { message: 'Произошла неизвестная ошибка при входе' };
  }
};

const login = async (values: LoginValues) => {
  try {
    const response: AxiosResponse<string, any> = await api.post(endpoints.login, values);
    if (response.data) {
      TokenService.setToken(response.data);
    }
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.message || 'Произошла ошибка при входе' };
    }
    return { message: 'Произошла неизвестная ошибка при входе' };
  }
};

const refresh = async () => {
  const response: AxiosResponse<string, any> = await api.post(endpoints.refresh);
  if (response.data) {
    TokenService.setToken(response.data);
  }
  return response;
};

const logout = async () => {
  const response: AxiosResponse<null, any> = await api.post(endpoints.logout);
  TokenService.removeToken();
  return response;
};

const AuthService = {
  register,
  login,
  refresh,
  logout,
};

export default AuthService;
