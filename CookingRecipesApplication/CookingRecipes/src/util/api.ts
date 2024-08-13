/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { AxiosError } from 'axios';
import TokenService from '../Services/TokenService';
import AuthService from '../Services/AuthService';
import { BASE_URL } from '../Constants/httpUrl';
import { errorToast } from '../Components/Toast/Toast';

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
  },
  withCredentials: true,
});

api.interceptors.request.use(
  (config) => {
    const token = TokenService.getAccessToken();
    if (token) {
      config.headers['Authorization'] = 'Bearer ' + token;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

api.interceptors.response.use(
  (res) => {
    return res;
  },
  async (err) => {
    const token = TokenService.getAccessToken();
    const originalConfig = err.config;

    if (!err.response) {
      alert('Не удалось подключиться к серверу. Пожалуйста, проверьте ваше интернет-соединение или попробуйте позже.');
      if (token) {
        TokenService.removeToken();
      }
      location.reload();
    }

    if (err.response) {
      if (err.response.status === 400 || err.response.status === 403) {
        if (err instanceof AxiosError) {
          err.response?.data?.errors.forEach((message: string) => {
            errorToast(message);
          });
        }
      }

      if (err.response.status === 401 && !originalConfig._retry) {
        originalConfig._retry = true;

        try {
          await AuthService.refresh();
          return api(originalConfig);
        } catch (_error) {
          await AuthService.logout();
          location.reload();
        }
      }
    }
    return Promise.reject(err);
  },
);

export default api;
