import { AxiosResponse } from 'axios';
import api from '../util/api';
import GetRecipesResponseValues from '../Types/GetRecipesResponseValues';
import GetRecipeByIdResponseValues from '../Types/GetRecipeByIdResponseValues';
import GetMostLikedRecipeResponseValue from '../Types/GetMostLikedRecipeResponseValue';
import { handleError } from '../Helpers/ErrorHandler';

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
    const response: AxiosResponse<GetRecipesResponseValues[], any> = await api.get(
      `${endpoints.getRecipes}${pageNumber}&searchString=${searchString}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetFavouriteRecipes = async (pageNumber: number) => {
  try {
    const response: AxiosResponse<GetRecipesResponseValues[], any> = await api.get(
      `${endpoints.getFavouriteRecipes}${pageNumber}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetMostLikedRecipe = async () => {
  try {
    const response: AxiosResponse<GetMostLikedRecipeResponseValue, any> = await api.get(
      `${endpoints.getMostLikedRecipe}`,
    );
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const GetRecipeById = async (recipeId: string) => {
  try {
    const response: AxiosResponse<GetRecipeByIdResponseValues, any> = await api.get(
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
