export default interface MostLikedRecipeResponseValue {
  id: number;
  name: string;
  description: string;
  avatarPath: string | null;
  authorName: string;
  cookingTime: number;
  likeCount: number;
}
