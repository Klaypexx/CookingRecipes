export default interface CardProps {
  className?: string;
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
  };
}
