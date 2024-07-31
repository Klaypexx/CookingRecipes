import React from 'react';
import styles from './CreateRecipe.module.css';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';

const CreateRecipe: React.FC = () => {
  return (
    <section className={styles.formSection}>
      <RecipeForm />
    </section>
  );
};

export default CreateRecipe;
