import RecipeFormValues from './RecipeFormValues';

export default interface RecipeFormProps {
  onSubmit: (formData: FormData, recipeId?: string) => any;
  values?: RecipeFormValues;
  toastMessage: string;
}
