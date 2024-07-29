import api from '../util/api';

const endpoints = {
  create: '/recipes/create',
  getRecipe: '/recipes/getall',
};

export interface TagDto {
  Name: string;
}

export interface IngredientDto {
  Name: string;
  Product: string;
}

export interface StepDto {
  Description: string;
}

export interface RecipeDto {
  Name: string;
  Description: string;
  CookingTime: number;
  Portion: number;
  Avatar: string;
  Tags: TagDto[];
  Ingredients: IngredientDto[];
  Steps: StepDto[];
}

const createRecipe = async (recipeData: RecipeDto) => {
  const response = await api.post(endpoints.create, recipeData);
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
