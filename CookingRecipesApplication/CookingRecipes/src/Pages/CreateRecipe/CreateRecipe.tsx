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
    avatar: 'avatarImage',
    tags: [],
    cookingTime: 0,
    portion: 0,
    step: [''],
    ingredient: [
      {
        header: '',
        products: '',
      },
    ],
  };

  const handleSubmit = async (values: RecipeFormValues) => {
    try {
      const recipeData = {
        Name: values.name,
        Description: values.description,
        CookingTime: values.cookingTime,
        Portion: values.portion,
        Avatar: values.avatar,
        Tags: values.tags.map((tag) => ({ Description: tag })),
        Ingredients: values.ingredient.map((ingredient) => ({
          Name: ingredient.header,
          Product: ingredient.products,
        })),
        Steps: values.step.map((step) => ({ Description: step })),
      };

      const result = await RecipeService.createRecipe(recipeData);
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
            <IngredientField name="ingredient" />
          </div>
          <div className={styles.stepBlock}>
            <StepField name="step" />
          </div>
        </div>
      </BaseForm>
    </section>
  );
};

export default CreateRecipe;
