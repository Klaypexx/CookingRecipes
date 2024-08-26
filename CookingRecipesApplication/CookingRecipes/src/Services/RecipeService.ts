import { AxiosError, AxiosResponse } from 'axios';
import api from '../util/api';
import GetRecipesResponseValues from '../Types/GetRecipesResponseValues';
import GetRecipeByIdResponseValues from '../Types/GetRecipeByIdResponseValues';

const endpoints = {
  create: '/recipes/',
  update: '/recipes/',
  remove: '/recipes/',
  getRecipes: '/recipes?pageNumber=',
  getFavouriteRecipes: '/recipes/favourites?pageNumber=',
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

const GetRecipes = async (pageNumber: number, searchString: string) => {
  try {
    const response: AxiosResponse<GetRecipesResponseValues[], any> = await api.get(
      `${endpoints.getRecipes}${pageNumber}&searchString=${searchString}`,
    );
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const GetFavouriteRecipes = async (pageNumber: number) => {
  try {
    const response: AxiosResponse<GetRecipesResponseValues[], any> = await api.get(
      `${endpoints.getFavouriteRecipes}${pageNumber}`,
    );
    return { response };
  } catch (error) {
    if (error instanceof AxiosError) {
      throw Error(error.response?.data?.errors);
    }
    throw Error('Произошла неизвестная ошибка при входе');
  }
};

const GetRecipeById = async (recipeId: string) => {
  try {
    const response: AxiosResponse<GetRecipeByIdResponseValues, any> = await api.get(
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
  GetRecipes,
  GetFavouriteRecipes,
  GetRecipeById,
};

export default RecipeService;
