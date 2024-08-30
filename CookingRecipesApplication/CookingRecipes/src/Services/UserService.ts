import { AxiosResponse } from 'axios';
import api from '../util/api';
import UsernameResponseValues from '../Types/UsernameResponseValues';
import { handleError } from '../Helpers/ErrorHandler';
import UserResponseValues from '../Types/UserResponseValues';

const endpoints = {
  getUser: '/users',
  username: '/users/username',
};

const getUser = async () => {
  try {
    const response: AxiosResponse<UserResponseValues, any> = await api.get(`${endpoints.getUser}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const username = async () => {
  try {
    const response: AxiosResponse<UsernameResponseValues, any> = await api.get(endpoints.username);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const UserService = {
  getUser,
  username,
};

export default UserService;
