import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import RecipeViewBlock from '../../Components/Recipe/RecipeView/RecipeView';
import Spinner from '../../Components/Spinner/Spinner';
import Subheader from '../../Components/Subheader/Subheader';
import { successToast } from '../../Components/Toast/Toast';
import removeIcon from '../../resources/icons/remove.svg';
import RecipeService from '../../Services/RecipeService';
import useAuthStore from '../../Stores/useAuthStore';
import useUserStore from '../../Stores/useUserStore';
import RecipeViewValues from '../../Types/RecipeViewValues';
import styles from './RecipeView.module.css';

const RecipeView = () => {
  let [loading, setLoading] = useState(true);
  const [isRecipeOwner, setIsRecipeOwner] = useState(false);
  const { userName } = useUserStore();
  const [values, setValues] = useState<RecipeViewValues>();
  const { recipeId } = useParams();
  const navigate = useNavigate();
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  //Фетчинг рецептов
  useEffect(() => {
    setLoading(true);
    const fetchRecipes = async () => {
      await RecipeService.GetRecipeById(recipeId!)
        .then(async (res) => {
          if (res) {
            setValues(res.response.data);

            const authorName = res.response.data.authorName;

            if (isAuthorized) {
              setIsRecipeOwner(authorName === userName);
            }

            setLoading(false);
          }
        })
        .catch(() => {
          navigate(-1);
        });
    };
    fetchRecipes();
  }, [isAuthorized]);

  const handleRemove = async () => {
    await RecipeService.removeRecipe(recipeId!).then((res) => {
      if (res) {
        successToast('Рецепт успешно удален');
        navigate(-1);
      }
    });
  };

  return (
    <div className={styles.recipeView}>
      <section>
        <Subheader backward text={values?.name}>
          <div className={styles.buttonBox}>
            {isRecipeOwner && (
              <>
                <BaseButton className={styles.removeRecipe} onClick={handleRemove}>
                  <img src={removeIcon} alt="removeIcon" className={styles.removeIcon} />
                </BaseButton>
                <BaseLink primary text="Редактировать" className={styles.editBtn} to={`/recipes/edit/${recipeId}`} />
              </>
            )}
          </div>
        </Subheader>
      </section>

      {loading ? (
        <Spinner />
      ) : (
        <section>
          <RecipeViewBlock values={values!} recipeId={recipeId!} />
        </section>
      )}
    </div>
  );
};

export default RecipeView;
