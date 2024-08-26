export default interface CustomCardProps {
  props: {
    name: string;
    description: string;
    avatarPath?: string;
    authorName: string;
    cookingTime: number;
    likeCount: number;
  };
}
