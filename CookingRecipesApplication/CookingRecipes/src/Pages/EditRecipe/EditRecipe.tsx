import RecipeForm from '../../Components/Form/RecipeForm/RecipeForm';
import RecipeService from '../../Services/RecipeService';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Subheader from '../../Components/Subheader/Subheader';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import Spinner from '../../Components/Spinner/Spinner';
import EditRecipeValues from '../../Types/EditRecipeValues';
import UserService from '../../Services/UserService';

const EditRecipe = () => {
  const [values, setValues] = useState<EditRecipeValues>();
  const { recipeId } = useParams();
  let [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchRecipes = async () => {
      await RecipeService.GetRecipeById(recipeId!)
        .then(async (res) => {
          if (res && res.response.status == 200) {
            const authorName = res.response.data.authorName;
            setValues(res.response.data);

            await UserService.username().then((res) => {
              if (res) {
                const userName = res.response.data.userName;
                if (authorName !== userName) {
                  navigate(-1);
                }
              }
            });
          } else {
            navigate(-1);
          }
        })
        .catch(() => {
          navigate(-1);
        });
      setLoading(false);
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
          {!loading && <BaseButton primary type="submit" form="form-submit" buttonText="Редактировать" />}
        </Subheader>
      </section>
      <section>
        {!values || loading ? (
          <Spinner />
        ) : (
          <RecipeForm onSubmit={handleEditRecipe} toastMessage="Рецепт успешно обновлен" values={values} />
        )}
      </section>
    </>
  );
};

export default EditRecipe;
