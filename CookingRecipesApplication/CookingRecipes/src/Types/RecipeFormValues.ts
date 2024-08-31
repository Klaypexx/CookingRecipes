export default interface RecipeFormValues {
  name: string;
  description: string;
  avatar?: File | null;
  avatarPath: string | null;
  tags?: Array<{
    name: string;
  }>;
  cookingTime: number;
  portion: number;
  steps: Array<{
    description: string;
  }>;
  ingredients: Array<{
    name: string;
    product: string;
  }>;
}
