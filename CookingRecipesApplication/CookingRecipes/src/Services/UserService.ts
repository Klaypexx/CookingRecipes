import { AxiosResponse } from 'axios';
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
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const UserService = {
  username,
};

export default UserService;
