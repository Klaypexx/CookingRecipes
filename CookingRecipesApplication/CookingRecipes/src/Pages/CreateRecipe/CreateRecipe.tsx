import React from 'react';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseForm from '../../Components/Form/BaseForm/BaseForm';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './CreateRecipe.module.css';
import { RecipeFormValues } from '../../Types/types';
import IngredientField from '../../Components/Form/IngredientField/IngredientField';
import CardField from '../../Components/Form/CardField/CardField';

const CreateRecipe: React.FC = () => {
  const initialValues: RecipeFormValues = {
    recipeName: '',
    description: '',
    tags: [],
    cookingTime: '',
    portion: '',
    step: [],
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
        <BaseCard margin>
          <CardField />
        </BaseCard>
        <div className={styles.mainFormContainer}>
          <IngredientField name="ingredient" />
          <div className={styles.stepFormBlock}>step</div>
        </div>
      </BaseForm>
    </section>
  );
};

export default CreateRecipe;
