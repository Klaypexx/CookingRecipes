import api from '../util/api';

const endpoints = {
  create: '/recipes/create',
  getRecipe: '/recipes/get',
  getCurrentRecipe: '/recipes/get/',
};

const createRecipe = async (recipeData: FormData) => {
  const response = await api.post(endpoints.create, recipeData, {
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
  const response = await api.post(endpoints.getRecipe, pages);
  if (response.data && response.data !== undefined) {
    return response.data;
  }
  return response.statusText;
};

const getCurrentUserRecipe = async (recipeId: string) => {
  const response = await api.post(endpoints.getCurrentRecipe + recipeId, recipeId);
  if (response.data && response.data !== undefined) {
    return response.data;
  }
  return response.statusText;
};

const RecipeService = {
  createRecipe,
  getAllRecipes,
  getCurrentUserRecipe,
};

export default RecipeService;
