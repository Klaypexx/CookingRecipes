import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import { useEffect, useState } from 'react';
import RecipeFormValues from '../../Types/RecipeFormValues';
import { useParams } from 'react-router-dom';
import Subheader from '../../Components/Subheader/Subheader';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import Spinner from '../../Components/Spinner/Spinner';

const EditRecipe = () => {
  const [values, setValues] = useState<RecipeFormValues>();
  const { recipeId } = useParams();

  useEffect(() => {
    const fetchRecipes = async () => {
      const result = await RecipeService.GetRecipeById(recipeId!);
      if (result.response && result.response.status === 200) {
        setValues(result.response.data);
      }
    };
    fetchRecipes();
  }, []);

  const handleEditRecipe = async (formData: FormData) => {
    return await RecipeService.editRecipe(formData, recipeId!);
  };

  return (
    <>
      <section>
        <Subheader backward text={'Редактировать рецепт'}>
          <BaseButton primary type="submit" form="form-submit" buttonText="Редактировать" />
        </Subheader>
      </section>
      <section>
        {!values ? (
          <Spinner />
        ) : (
          <RecipeForm onSubmit={handleEditRecipe} toastMessage="Рецепт успешно обновлен" values={values} />
        )}
      </section>
    </>
  );
};

export default EditRecipe;
