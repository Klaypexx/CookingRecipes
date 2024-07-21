/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import TokenService from "../Services/TokenService";
import AuthService from "../Services/AuthService";

export const navigation = (navigate: any) => {
  return navigate;
}

const api = axios.create({
  baseURL: "http://localhost:5014",
  headers: {
    "Content-Type": "application/json",
    'Access-Control-Allow-Origin': '*',
  },
  withCredentials: true, 
});

api.interceptors.request.use(
  (config) => {
    const token = TokenService.getAccessToken();
    console.log(token)
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
      navigation(0)
    }

    if (err.response) {
      if (err.response.status === 400) {
        console.log("Перехватил ошибку 400")
      }

      if (err.response.status === 401 && !originalConfig._retry) {
        originalConfig._retry = true;

        try {
          const rs = await AuthService.refresh();
          const accessToken = rs.data;
          TokenService.updateAccessToken(accessToken);
          return api(originalConfig);
        } catch (_error) {
          TokenService.removeToken();
          navigation(0)
        }
      }
    }
    return Promise.reject(err);
  }
);

export default api;