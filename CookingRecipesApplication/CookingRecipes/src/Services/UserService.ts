import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';
import UsernameResponseValues from '../Types/UsernameResponseValues';

const endpoints = {
  username: '/users/username',
};

const username = async () => {
  try {
    const response: AxiosResponse<UsernameResponseValues, any> = await api.get(endpoints.username);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.message || 'Произошла ошибка при запросе' };
    }
    return { message: 'Произошла неизвестная ошибка при запросе' };
  }
};

const UserService = {
  username,
};

export default UserService;
