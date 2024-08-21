export default interface RecipeViewValues {
  name: string;
  description: string;
  avatarPath?: string;
  authorName: string;
  isLike: boolean;
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
