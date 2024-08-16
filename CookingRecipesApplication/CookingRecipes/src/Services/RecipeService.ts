import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';
import GetAllRecipesResponseValues from '../Types/GetAllRecipesResponseValues';
import GetCurrentUserRecipeResponseValues from '../Types/GetCurrentUserRecipeResponseValues';

const endpoints = {
  create: '/recipes/',
  update: '/recipes/',
  remove: '/recipes/',
  getRecipe: '/recipes?pageNumber=',
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
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
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
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const removeRecipe = async (recipeId: string) => {
  try {
    const response: AxiosResponse<null, any> = await api.delete(`${endpoints.remove}${recipeId}`);
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
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
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
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
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const RecipeService = {
  createRecipe,
  editRecipe,
  removeRecipe,
  GetRecipesForPage,
  getCurrentUserRecipe,
};

export default RecipeService;
