import api from '../util/api';

const endpoints = {
  create: '/recipes/create',
  getRecipe: '/recipes/getall',
};

export interface TagDto {
  Name: string;
}

const createRecipe = async (recipeData: FormData) => {
  const response = await api.post(endpoints.create, recipeData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  if (response.data && response.data !== undefined) {
    return response.data;
  }
  return response.statusText;
};

const getAllUserRecipes = async () => {
  const response = await api.get(endpoints.getRecipe);
  if (response.data && response.data !== undefined) {
    console.log(response.data);
  }
  return response.statusText;
};

const RecipeService = {
  createRecipe,
  getAllUserRecipes,
};

export default RecipeService;
