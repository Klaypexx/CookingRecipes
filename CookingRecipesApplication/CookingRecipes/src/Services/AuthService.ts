import { AxiosResponse } from 'axios';
import api from '../util/api';
import TokenService from './TokenService';
import RegisterValues from '../Types/RegisterValues';
import LoginValues from '../Types/LoginValues';
import { handleError } from '../Helpers/ErrorHandler';

const endpoints = {
  login: '/users/login',
  register: '/users/register',
  refresh: '/users/refresh',
  logout: '/users/logout',
  isAuth: '/users/isAuth',
};

const register = async (values: RegisterValues) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(endpoints.register, values);
    return { response };
  } catch (error) {
    handleError(error);
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
    handleError(error);
  }
};

const refresh = async () => {
  try {
    const response: AxiosResponse<string, any> = await api.post(endpoints.refresh);
    if (response.data) {
      TokenService.updateAccessToken(response.data);
    }
    return response;
  } catch (error) {
    handleError(error);
    throw error;
  }
};

const logout = async () => {
  try {
    const response: AxiosResponse<null, any> = await api.post(endpoints.logout);
    TokenService.removeToken();
    return response;
  } catch (error) {
    handleError(error);
  }
};

const isAuth = async () => {
  try {
    await api.post(endpoints.isAuth);
  } catch (error) {
    handleError(error);
  }
};

const AuthService = {
  register,
  login,
  refresh,
  logout,
  isAuth,
};

export default AuthService;
