export default interface RecipeListValues {
  id: number;
  name: string;
  description: string;
  avatarPath?: string;
  authorName: string;
  tags?: Array<{
    name: string;
  }>;
  cookingTime: number;
  portion: number;
  isLike: boolean;
  likeCount: number;
}
