import React from 'react';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseForm from '../../Components/Form/BaseForm/BaseForm';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './CreateRecipe.module.css';
import { RecipeFormValues } from '../../Types/types';
import IngredientField from '../../Components/Field/IngredientField/IngredientField';
import CardField from '../../Components/Field/CardField/CardField';
import StepField from '../../Components/Field/StepField/StepField';

const CreateRecipe: React.FC = () => {
  const initialValues: RecipeFormValues = {
    name: '',
    description: '',
    avatar: '',
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

  const handleSubmit = (values: RecipeFormValues) => {
    console.log(values);
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
