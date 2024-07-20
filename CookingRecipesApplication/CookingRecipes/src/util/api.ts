/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import TokenService from "../Services/TokenService";
import AuthService from "../Services/AuthService";

const api = axios.create({
  baseURL: "http://localhost:5014",
  headers: {
    "Content-Type": "application/json",
    'Access-Control-Allow-Origin': '*',
  },
  withCredentials: true, 
});

// Создайте функцию для настройки интерсепторов
export const setupInterceptors = (navigate: any) => {
  api.interceptors.request.use(
    (config) => {
      const token = TokenService.getAccessToken();
      if (token) {
        config.headers["Authorization"] = 'Bearer ' + token; 
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  api.interceptors.response.use(
    (res) => {
      return res;
    },
    async (err) => {
      const token = TokenService.getAccessToken();
      const originalConfig = err.config;

      if (!err.response) {
        alert("Не удалось подключиться к серверу. Пожалуйста, проверьте ваше интернет-соединение или попробуйте позже.");
        if (token) {
          TokenService.removeToken();
        }
        navigate(0);
      }

      if (err.response) {
        if (err.response.status === 400) {
          console.log("Плохой запрос")
          if (token) {
            TokenService.removeToken();
          }
          navigate(0);
        }

        if (err.response.status === 401 && !originalConfig._retry) {
          originalConfig._retry = true;

          try {
            console.log("Я в ревреше")
            const rs = await AuthService.refresh();
            const accessToken = rs.data;
            TokenService.updateAccessToken(accessToken);
            return api(originalConfig);
          } catch (_error) {
            TokenService.removeToken();
            // navigate(0); // Перенаправляем на страницу логина
            console.log(_error);
          }
        }
      }
      return Promise.reject(err);
    }
  );
};

export default api;