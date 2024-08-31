export default interface CustomCardProps {
  props: {
    name: string;
    description: string;
    avatarPath: string | null;
    authorName: string;
    cookingTime: number;
    likeCount: number;
  };
}
