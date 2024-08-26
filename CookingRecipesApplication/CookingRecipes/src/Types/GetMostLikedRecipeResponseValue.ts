export default interface GetMostLikedRecipeResponseValue {
  id: number;
  name: string;
  description: string;
  avatarPath?: string;
  authorName: string;
  cookingTime: number;
  likeCount: number;
}
