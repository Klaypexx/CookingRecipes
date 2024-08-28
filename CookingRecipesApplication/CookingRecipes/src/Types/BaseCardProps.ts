export default interface BaseCardProps {
  className?: string;
  recipeId: string;
  props?: {
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
    isFavourite: boolean;
    favouriteCount: number;
  };
}
