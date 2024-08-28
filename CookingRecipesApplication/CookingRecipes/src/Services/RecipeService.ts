import { AxiosResponse } from 'axios';
import api from '../util/api';
import RecipeResponseValues from '../Types/RecipeResponseValues';
import RecipeByIdResponseValues from '../Types/RecipeByIdResponseValues';
import MostLikedRecipeResponseValue from '../Types/MostLikedRecipeResponseValue';
import { handleError } from '../Helpers/ErrorHandler';
import FavouriteRecipeResponseValues from '../Types/FavouriteRecipeResponseValues';

const endpoints = {
  create: '/recipes/',
  update: '/recipes/',
  remove: '/recipes/',
  getRecipes: '/recipes?pageNumber=',
  getFavouriteRecipes: '/recipes/favourites?pageNumber=',
  getMostLikedRecipe: '/recipes/mostLiked',
  getCurrentRecipe: '/recipes/',
};

const createRecipe = async (values: FormData) => {
  try {
    const response: AxiosResponse<null, any> = await api.post(endpoints.create, values, {
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

const editRecipe = async (values: FormData, recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.put(`${endpoints.update}${recipeId}`, values, {
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

const removeRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.delete(`${endpoints.remove}${recipeId}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetRecipes = async (pageNumber: number, searchString: string) => {
  try {
    const response: AxiosResponse<RecipeResponseValues[], any> = await api.get(
      `${endpoints.getRecipes}${pageNumber}&searchString=${searchString}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetFavouriteRecipes = async (pageNumber: number) => {
  try {
    const response: AxiosResponse<FavouriteRecipeResponseValues[], any> = await api.get(
      `${endpoints.getFavouriteRecipes}${pageNumber}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetMostLikedRecipe = async () => {
  try {
    const response: AxiosResponse<MostLikedRecipeResponseValue | null, any> = await api.get(
      `${endpoints.getMostLikedRecipe}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetRecipeById = async (recipeId: string) => {
  try {
    const response: AxiosResponse<RecipeByIdResponseValues, any> = await api.get(
      `${endpoints.getCurrentRecipe}${recipeId}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const RecipeService = {
  createRecipe,
  editRecipe,
  removeRecipe,
  GetRecipes,
  GetFavouriteRecipes,
  GetMostLikedRecipe,
  GetRecipeById,
};

export default RecipeService;
