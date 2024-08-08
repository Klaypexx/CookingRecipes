import styles from './EditRecipe.module.css';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import { useEffect, useState } from 'react';
import RecipeFormValues from '../../Types/RecipeFormValues';
import { useParams } from 'react-router-dom';

const EditRecipe = () => {
  const [values, setValues] = useState<RecipeFormValues>();
  const { recipeId } = useParams();

  useEffect(() => {
    const fetchRecipes = async () => {
      const result = await RecipeService.getCurrentUserRecipe(recipeId!);
      if (result.response && result.response.status === 200) {
        setValues(result.response.data);
      } else {
        throw Error(result.message);
      }
    };
    fetchRecipes();
  }, []);

  return (
    <section className={styles.formSection}>
      {values && (
        <RecipeForm
          onSubmit={RecipeService.editRecipe}
          toastMessage="Рецепт успешно обновлен"
          headerText="Редактировать рецепт"
          values={values}
        />
      )}
    </section>
  );
};

export default EditRecipe;
