export default interface RecipeByIdResponseValues {
  name: string;
  description: string;
  avatarPath: string | null;
  authorName: string;
  isLike: boolean;
  likeCount: number;
  isFavourite: boolean;
  favouriteCount: number;
  tags:
    | Array<{
        name: string;
      }>
    | [];
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
