import { AxiosResponse } from 'axios';
import api from '../util/api';
import { handleError } from '../Helpers/ErrorHandler';

const endpoints = {
  addFavouriteRecipe: '/favourites?recipeId=',
  removeFavouriteRecipe: '/favourites?recipeId=',
};

const addFavouriteRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(`${endpoints.addFavouriteRecipe}${recipeId}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const removeFavouriteRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.delete(`${endpoints.removeFavouriteRecipe}${recipeId}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const FavouriteRecipeService = {
  addFavouriteRecipe,
  removeFavouriteRecipe,
};

export default FavouriteRecipeService;
