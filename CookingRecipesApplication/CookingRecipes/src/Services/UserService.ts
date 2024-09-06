import { AxiosResponse } from 'axios';
import { handleError } from '../Helpers/ErrorHandler';
import NameOfUserResponseValues from '../Types/NameOfUserResponseValues';
import UsernameResponseValues from '../Types/UsernameResponseValues';
import UserResponseValues from '../Types/UserResponseValues';
import UserStatisticResponseValues from '../Types/UserStatisticResponseValues';
import api from '../util/api';

const endpoints = {
  update: '/users',
  getUser: '/users',
  username: '/users/username',
  nameOfUser: '/users/name',
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

const nameOfUser = async () => {
  try {
    const response: AxiosResponse<NameOfUserResponseValues, any> = await api.get(endpoints.nameOfUser);
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
  nameOfUser,
  getUserStatistic,
};

export default UserService;
