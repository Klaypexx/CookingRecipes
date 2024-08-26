export default interface GetRecipeByIdResponseValues {
  name: string;
  description: string;
  avatarPath?: string;
  authorName: string;
  isLike: boolean;
  likeCount: number;
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
