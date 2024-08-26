import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';

const endpoints = {
  addLike: '/likes?recipeId=',
  removeLike: '/likes?recipeId=',
};

const addLike = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(`${endpoints.addLike}${recipeId}`);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const removeLike = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.delete(`${endpoints.removeLike}${recipeId}`);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const LikeService = {
  addLike,
  removeLike,
};

export default LikeService;
