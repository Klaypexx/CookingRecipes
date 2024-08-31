export default interface EditRecipeValues {
  name: string;
  description: string;
  avatarPath: string | null;
  tags:
    | Array<{
        name: string;
      }>
    | [];
  cookingTime: number;
  portion: number;
  steps: Array<{
    description: string;
  }>;
  ingredients: Array<{
    name: string;
    product: string;
  }>;
}
