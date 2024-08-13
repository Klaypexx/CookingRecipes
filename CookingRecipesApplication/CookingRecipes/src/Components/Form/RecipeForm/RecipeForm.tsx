import { useLocation, useNavigate } from 'react-router-dom';
import { successToast } from '../../Toast/Toast';
import styles from './RecipeForm.module.css';
import BaseForm from '../BaseForm/BaseForm';
import CardField from '../../Field/CardField/CardField';
import IngredientField from '../../Field/IngredientField/IngredientField';
import StepField from '../../Field/StepField/StepField';
import recipeValidation from './RecipeValidation';
import RecipeFormValues from '../../../Types/RecipeFormValues';
import { AxiosResponse } from 'axios';

interface RecipeFormProps {
  onSubmit: (formData: FormData, recipeId?: string) => Promise<{ response?: AxiosResponse; message?: string }>;
  values?: RecipeFormValues;
  toastMessage: string;
}

const RecipeForm: React.FC<RecipeFormProps> = ({ onSubmit, values, toastMessage }) => {
  const location = useLocation();
  const navigate = useNavigate();
  const initialValues: RecipeFormValues = values
    ? values
    : {
        name: '',
        description: '',
        avatar: undefined,
        avatarPath: undefined,
        tags: undefined,
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
    let formData = new FormData();
    formData.append('Name', values.name);
    formData.append('Description', values.description);
    formData.append('CookingTime', values.cookingTime.toString());
    formData.append('Portion', values.portion.toString());

    if (values.avatar) {
      formData.append('Avatar', values.avatar);
    }

    if (values.tags) {
      values.tags.forEach((tag, index) => {
        formData.append(`Tags[${index}].Name`, tag.name);
      });
    }

    values.ingredients.forEach((ingredient, index) => {
      formData.append(`Ingredients[${index}].Name`, ingredient.name);
      formData.append(`Ingredients[${index}].Product`, ingredient.product);
    });

    values.steps.forEach((step, index) => {
      formData.append(`Steps[${index}].Description`, step.description);
    });

    const result = await onSubmit(formData);
    if (result.response && result.response.status === 200) {
      successToast(toastMessage);
      navigate(-1);
    }
  };

  return (
    <>
      <BaseForm
        id="form-submit"
        initialValues={initialValues}
        validationSchema={recipeValidation}
        onSubmit={handleSubmit}
      >
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
    </>
  );
};

export default RecipeForm;
