import { AxiosResponse } from 'axios';
import api from '../util/api';
import UsernameResponseValues from '../Types/UsernameResponseValues';
import { handleError } from '../Helpers/ErrorHandler';
import UserResponseValues from '../Types/UserResponseValues';
import UserStatisticResponseValues from '../Types/UserStatisticResponseValues';

const endpoints = {
  update: '/users',
  getUser: '/users',
  username: '/users/username',
  staticstic: '/users/statistic',
};

const updateUser = async (values: FormData) => {
  try {
    const response: AxiosResponse<null, any> = await api.put(`${endpoints.update}`, values, {
      headers: {
        'Content-Type': 'multipart/form-data',
        'Access-Control-Allow-Origin': '*',
      },
    });
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const getUser = async () => {
  try {
    const response: AxiosResponse<UserResponseValues, any> = await api.get(`${endpoints.getUser}`);
    console.log(response.data);
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

const getUserStatistic = async () => {
  try {
    const response: AxiosResponse<UserStatisticResponseValues, any> = await api.get(`${endpoints.staticstic}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const UserService = {
  updateUser,
  getUser,
  username,
  getUserStatistic,
};

export default UserService;
