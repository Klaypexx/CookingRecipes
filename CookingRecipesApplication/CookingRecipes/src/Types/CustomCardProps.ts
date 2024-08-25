export default interface CustomCardProps {
  props?: {
    name: string;
    description: string;
    avatarPath?: string;
    authorName: string;
    tags?: Array<{
      name: string;
    }>;
    cookingTime: number;
    isLike: boolean;
    likeCount: number;
  };
}
