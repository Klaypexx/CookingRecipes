import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Subheader from '../../Components/Subheader/Subheader';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import Spinner from '../../Components/Spinner/Spinner';
import EditRecipeValues from '../../Types/EditRecipeValues';

const EditRecipe = () => {
  const [values, setValues] = useState<EditRecipeValues>();
  const { recipeId } = useParams();

  useEffect(() => {
    const fetchRecipes = async () => {
      await RecipeService.GetRecipeById(recipeId!).then((res) => {
        if (res) {
          setValues(res.response.data);
        }
      });
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
