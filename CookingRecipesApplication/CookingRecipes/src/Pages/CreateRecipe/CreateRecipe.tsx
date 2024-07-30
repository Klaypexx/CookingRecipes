import React from 'react';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseForm from '../../Components/Form/BaseForm/BaseForm';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './CreateRecipe.module.css';
import { RecipeFormValues } from '../../Types/types';
import IngredientField from '../../Components/Field/IngredientField/IngredientField';
import CardField from '../../Components/Field/CardField/CardField';
import StepField from '../../Components/Field/StepField/StepField';
import RecipeService from '../../Services/RecipeService';

const CreateRecipe: React.FC = () => {
  const initialValues: RecipeFormValues = {
    name: '',
    description: '',
    avatar: null,
    tags: [],
    cookingTime: 0,
    portion: 0,
    steps: [''],
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
        formData.append(`Steps[${index}].Description`, step);
      });

      const result = await RecipeService.createRecipe(formData);
      console.log('Recipe created:', result);
    } catch (error) {
      console.error('Error creating recipe:', error);
    }
  };

  return (
    <section className={styles.formSection}>
      <BaseForm initialValues={initialValues} onSubmit={handleSubmit}>
        <Subheader backward btn type="submit" buttonText="Опубликовать" headerText="Добавить новый рецепт" />
        <BaseCard form margin>
          <CardField />
        </BaseCard>
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
    </section>
  );
};

export default CreateRecipe;
