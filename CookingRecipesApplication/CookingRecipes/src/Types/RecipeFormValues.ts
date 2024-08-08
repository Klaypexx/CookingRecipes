export default interface RecipeFormValues {
  name: string;
  description: string;
  avatar?: File | null;
  avatarUrl?: string | undefined;
  tags?: string[];
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
