import { AxiosResponse } from 'axios';
import { handleError } from '../Helpers/ErrorHandler';
import api from '../util/api';

const endpoints = {
  addLike: '/likes',
  removeLike: '/likes/',
};

const addLike = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(endpoints.addLike, recipeId);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const removeLike = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.delete(`${endpoints.removeLike}${recipeId}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const LikeService = {
  addLike,
  removeLike,
};

export default LikeService;
