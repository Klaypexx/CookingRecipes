import React from 'react';
import styles from './CreateRecipe.module.css';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';

const CreateRecipe: React.FC = () => {
  return (
    <section className={styles.formSection}>
      <RecipeForm
        onSubmit={RecipeService.createRecipe}
        toastMessage="Рецепт успешно создан"
        headerText="Добавить новый рецепт"
      />
    </section>
  );
};

export default CreateRecipe;
