import styles from './EditRecipe.module.css';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import { useState } from 'react';
import RecipeFormValues from '../../Types/RecipeFormValues';

const EditRecipe = () => {
  const [values, setValues] = useState<RecipeFormValues>();

  return (
    <section className={styles.formSection}>
      <RecipeForm
        onSubmit={RecipeService.editRecipe}
        toastMessage="Рецепт успешно обновлен"
        headerText="Редактировать рецепт"
      />
    </section>
  );
};

export default EditRecipe;
