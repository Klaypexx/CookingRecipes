export default interface GetAllRecipesResponseValues {
  name: string;
  description: string;
  avatarPath?: string;
  authorName: string;
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
