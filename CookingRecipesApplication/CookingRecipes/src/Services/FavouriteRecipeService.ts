import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';

const endpoints = {
  addFavouriteRecipe: '/favourites?recipeId=',
  removeFavouriteRecipe: '/favourites?recipeId=',
};

const addFavouriteRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(`${endpoints.addFavouriteRecipe}${recipeId}`);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const removeFavouriteRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.delete(`${endpoints.removeFavouriteRecipe}${recipeId}`);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const FavouriteRecipeService = {
  addFavouriteRecipe,
  removeFavouriteRecipe,
};

export default FavouriteRecipeService;
