import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';
import GetAllRecipesResponseValues from '../Types/GetAllRecipesResponseValues';
import GetCurrentUserRecipeResponseValues from '../Types/GetCurrentUserRecipeResponseValues';

const endpoints = {
  create: '/recipes/create',
  getRecipe: '/recipes/get',
  getCurrentRecipe: '/recipes/get/',
};

const createRecipe = async (values: FormData) => {
  const response: AxiosResponse<null, any> = await api.post(endpoints.create, values, {
    headers: {
      'Content-Type': 'multipart/form-data',
      'Access-Control-Allow-Origin': '*',
    },
  });
  if (response.data && response.data !== undefined) {
    return response.data;
  }
  return response.statusText;
};

const getAllRecipes = async (pages: number) => {
  try {
    const response: AxiosResponse<GetAllRecipesResponseValues[], any> = await api.get(endpoints.getRecipe, {
      headers: {
        page: pages,
      },
    });
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.message || 'Произошла ошибка при запросе' };
    }
    return { message: 'Произошла неизвестная ошибка при запросе' };
  }
};

const getCurrentUserRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<GetCurrentUserRecipeResponseValues, any> = await api.get(
      endpoints.getCurrentRecipe + recipeId,
      {
        headers: {
          recipeId: recipeId,
        },
      },
    );
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.message || 'Произошла ошибка при запросе' };
    }
    return { message: 'Произошла неизвестная ошибка при запросе' };
  }
};

const RecipeService = {
  createRecipe,
  getAllRecipes,
  getCurrentUserRecipe,
};

export default RecipeService;
