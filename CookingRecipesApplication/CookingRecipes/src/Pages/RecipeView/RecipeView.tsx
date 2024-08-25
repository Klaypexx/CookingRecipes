import Subheader from '../../Components/Subheader/Subheader';
import styles from './RecipeView.module.css';
import removeIcon from '../../resources/icons/remove.svg';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import RecipeService from '../../Services/RecipeService';
import RecipeViewValues from '../../Types/RecipeViewValues';
import { successToast } from '../../Components/Toast/Toast';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import UserService from '../../Services/UserService';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import useAuthStore from '../../Stores/useAuthStore';
import Spinner from '../../Components/Spinner/Spinner';

const RecipeView = () => {
  let [loading, setLoading] = useState(true);
  const [isRecipeOwner, setRecipeOwner] = useState(false);
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
      try {
        const result = await RecipeService.GetRecipeById(recipeId!);

        setValues(result.response.data);
        const authorName = result.response.data.authorName;

        if (isAuthorized) {
          const result = await UserService.username();

          const userName = result.response.data.userName;
          setRecipeOwner(authorName === userName);
        }
        setLoading(false);
      } catch {
        navigate(-1);
      }
    };
    fetchRecipes();
  }, [isAuthorized]);

  const handleRemove = async () => {
    const result = await RecipeService.removeRecipe(recipeId!);

    if (result.response && result.response.status === 200) {
      successToast('Рецепт успешно удален');
      navigate(-1);
    }
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
          <BaseCard props={values} recipeId={recipeId!} />
          <div className={styles.flexContainer}>
            <div>
              <h4 className={styles.ingredientsHeader}>Ингредиенты</h4>
              {values?.ingredients.map((ingredient, index) => (
                <div key={index} className={styles.ingredientBox}>
                  <p className={styles.ingredientsName}>{ingredient.name}</p>
                  <p className={styles.ingredientsText}>{ingredient.product}</p>
                </div>
              ))}
            </div>
            <div>
              {values?.steps.map((step, index) => (
                <div key={index} className={styles.stepBox}>
                  <p className={styles.stepHeader}>Шаг {index + 1}</p>
                  <div className={styles.stepTextBox}>
                    <p>{step.description}</p>
                  </div>
                </div>
              ))}
              <h3 className={styles.stepsMealEnjoyText}>Приятного Аппетита!</h3>
            </div>
          </div>
        </section>
      )}
    </div>
  );
};

export default RecipeView;
