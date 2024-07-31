import { useLocation, useNavigate } from 'react-router-dom';
import { RecipeFormValues } from '../../../Types/types';
import RecipeService from '../../../Services/RecipeService';
import { successToast } from '../../Toast/Toast';
import styles from './RecipeForm.module.css';
import BaseForm from '../BaseForm/BaseForm';
import Subheader from '../../Subheader/Subheader';
import BaseButton from '../../Button/BaseButton/BaseButton';
import CardField from '../../Field/CardField/CardField';
import IngredientField from '../../Field/IngredientField/IngredientField';
import StepField from '../../Field/StepField/StepField';
import recipeValidation from './RecipeValidation';

const RecipeForm = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const initialValues: RecipeFormValues = {
    name: '',
    description: '',
    avatar: null,
    tags: [],
    cookingTime: 0,
    portion: 0,
    steps: [
      {
        description: '',
      },
    ],
    ingredients: [
      {
        name: '',
        product: '',
      },
    ],
  };

  const handleSubmit = async (values: RecipeFormValues) => {
    try {
      let formData = new FormData();
      formData.append('Name', values.name);
      formData.append('Description', values.description);
      formData.append('CookingTime', values.cookingTime.toString());
      formData.append('Portion', values.portion.toString());
      if (values.avatar) {
        formData.append('Avatar', values.avatar);
      }

      values.tags.forEach((tag, index) => {
        formData.append(`Tags[${index}].Name`, tag);
      });

      values.ingredients.forEach((ingredient, index) => {
        formData.append(`Ingredients[${index}].Name`, ingredient.name);
        formData.append(`Ingredients[${index}].Product`, ingredient.product);
      });

      values.steps.forEach((step, index) => {
        formData.append(`Steps[${index}].Description`, step.description);
      });

      await RecipeService.createRecipe(formData);
      successToast('Рецепт успешно создан');
      navigate(location.state?.from);
    } catch (error) {
      console.error('Error creating recipe:', error);
    }
  };

  return (
    <BaseForm initialValues={initialValues} validationSchema={recipeValidation} onSubmit={handleSubmit}>
      <Subheader backward headerText="Добавить новый рецепт">
        <BaseButton primary type="submit" buttonText="Добавить рецепт" />
      </Subheader>
      <CardField />
      <div className={styles.mainContainer}>
        <div className={styles.ingredientBlock}>
          <h4 className={styles.ingredientHeader}>Ингридиенты</h4>
          <IngredientField name="ingredients" />
        </div>
        <div className={styles.stepBlock}>
          <StepField name="steps" />
        </div>
      </div>
    </BaseForm>
  );
};

export default RecipeForm;
