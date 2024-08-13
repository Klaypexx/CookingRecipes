import styles from './EditRecipe.module.css';
import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import { useEffect, useState } from 'react';
import RecipeFormValues from '../../Types/RecipeFormValues';
import { useParams } from 'react-router-dom';
import Subheader from '../../Components/Subheader/Subheader';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import Spinner from '../../Components/Spinner/Spinner';

const EditRecipe = () => {
  const [loading, setLoading] = useState(true);
  const [values, setValues] = useState<RecipeFormValues>();
  const { recipeId } = useParams();

  useEffect(() => {
    const fetchRecipes = async () => {
      const result = await RecipeService.getCurrentUserRecipe(recipeId!);
      if (result.response && result.response.status === 200) {
        setValues(result.response.data);
        setLoading(!loading);
      }
    };
    fetchRecipes();
  }, []);

  const handleEditRecipe = async (formData: FormData) => {
    return await RecipeService.editRecipe(formData, recipeId!);
  };

  if (loading) {
    return <Spinner />;
  }

  return (
    <section className={styles.formSection}>
      <Subheader backward headerText={loading ? undefined : 'Редактировать рецепт'}>
        <BaseButton primary type="submit" form="form-submit" buttonText="Редактировать" />
      </Subheader>
      {values && <RecipeForm onSubmit={handleEditRecipe} toastMessage="Рецепт успешно обновлен" values={values} />}
    </section>
  );
};

export default EditRecipe;
