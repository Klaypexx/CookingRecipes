import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';
import GetAllRecipesResponseValues from '../Types/GetAllRecipesResponseValues';
import GetCurrentUserRecipeResponseValues from '../Types/GetCurrentUserRecipeResponseValues';

const endpoints = {
  create: '/recipes/create',
  getRecipe: '/recipes/get/list/',
  getCurrentRecipe: '/recipes/get/',
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
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.errors || 'Произошла ошибка при запросе' };
    }
    return { message: 'Произошла неизвестная ошибка при запросе' };
  }
};

const GetRecipesForPage = async (pageNumber: number) => {
  try {
    const response: AxiosResponse<GetAllRecipesResponseValues[], any> = await api.get(
      `${endpoints.getRecipe}${pageNumber}`,
    );
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.errors || 'Произошла ошибка при запросе' };
    }
    return { message: 'Произошла неизвестная ошибка при запросе' };
  }
};

const getCurrentUserRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<GetCurrentUserRecipeResponseValues, any> = await api.get(
      `${endpoints.getCurrentRecipe}${recipeId}`,
    );
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      return { message: error.response?.data?.errors || 'Произошла ошибка при запросе' };
    }
    return { message: 'Произошла неизвестная ошибка при запросе' };
  }
};

const RecipeService = {
  createRecipe,
  GetRecipesForPage,
  getCurrentUserRecipe,
};

export default RecipeService;
