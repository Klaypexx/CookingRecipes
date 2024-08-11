export default interface GetAllRecipesResponseValues {
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
}
