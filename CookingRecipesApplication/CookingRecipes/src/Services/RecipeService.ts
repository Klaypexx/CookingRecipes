import { AxiosResponse } from 'axios';
import { handleError } from '../Helpers/ErrorHandler';
import FavouriteRecipeResponseValues from '../Types/FavouriteRecipeResponseValues';
import MostLikedRecipeResponseValue from '../Types/MostLikedRecipeResponseValue';
import RecipeByIdResponseValues from '../Types/RecipeByIdResponseValues';
import RecipeResponseValues from '../Types/RecipeResponseValues';
import RecipesDataResponse from '../Types/RecipesDataResponse';
import UserRecipeResponseValue from '../Types/UserRecipeResponseValues';
import api from '../util/api';

const endpoints = {
  create: '/recipes/',
  update: '/recipes/',
  remove: '/recipes/',
  getRecipes: '/recipes?pageNumber=',
  getFavouriteRecipes: '/recipes/favourites?pageNumber=',
  getUserRecipes: './recipes/userRecipes?pageNumber=',
  getMostLikedRecipe: '/recipes/liked',
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
    const response: AxiosResponse<RecipesDataResponse<RecipeResponseValues>, any> = await api.get(
      `${endpoints.getRecipes}${pageNumber}&searchString=${searchString}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetFavouriteRecipes = async (pageNumber: number) => {
  try {
    const response: AxiosResponse<RecipesDataResponse<FavouriteRecipeResponseValues>, any> = await api.get(
      `${endpoints.getFavouriteRecipes}${pageNumber}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetUserRecipes = async (pageNumber: number) => {
  try {
    const response: AxiosResponse<RecipesDataResponse<UserRecipeResponseValue>, any> = await api.get(
      `${endpoints.getUserRecipes}${pageNumber}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
    throw error;
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
    throw error;
  }
};

const RecipeService = {
  createRecipe,
  editRecipe,
  removeRecipe,
  GetRecipes,
  GetFavouriteRecipes,
  GetUserRecipes,
  GetMostLikedRecipe,
  GetRecipeById,
};

export default RecipeService;
