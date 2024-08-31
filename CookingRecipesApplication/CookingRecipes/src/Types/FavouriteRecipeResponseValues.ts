export default interface FavouriteRecipeResponseValues {
  id: number;
  name: string;
  description: string;
  avatarPath: string | null;
  authorName: string;
  tags:
    | Array<{
        name: string;
      }>
    | [];
  cookingTime: number;
  portion: number;
  isLike: boolean;
  likeCount: number;
  isFavourite: boolean;
  favouriteCount: number;
}
