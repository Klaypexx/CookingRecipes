import { AxiosResponse } from 'axios';
import api from '../util/api';
import UsernameResponseValues from '../Types/UsernameResponseValues';
import { handleError } from '../Helpers/ErrorHandler';

const endpoints = {
  username: '/users/username',
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
  username,
};

export default UserService;
