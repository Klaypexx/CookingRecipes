import Subheader from '../../Components/Subheader/Subheader';
import styles from './RecipeView.module.css';
import removeIcon from '../../resources/icons/remove.svg';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import { useEffect, useState } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import RecipeService from '../../Services/RecipeService';
import RecipeViewValues from '../../Types/RecipeViewValues';
import { successToast } from '../../Components/Toast/Toast';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import Spinner from '../../Components/Spinner/Spinner';
import UserService from '../../Services/UserService';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';

const RecipeView = () => {
  const [loading, setLoading] = useState(true);
  const [isRecipeOwner, setRecipeOwner] = useState(false);
  const [values, setValues] = useState<RecipeViewValues>();
  const { recipeId } = useParams();
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

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

  useEffect(() => {
    if (values) {
      const fetchUsername = async () => {
        const result = await UserService.username();
        if (result.response && result.response.status === 200) {
          const userName = result.response.data.userName;
          setRecipeOwner(values.authorName === userName);
        }
      };
      fetchUsername();
    }
  }, [values]);

  const handleRemove = async () => {
    const result = await RecipeService.removeRecipe(recipeId!);

    if (result.response && result.response.status === 200) {
      successToast('Рецепт успешно удален');
      navigate(location.state?.from);
    }
  };

  if (loading) {
    return <Spinner />;
  }

  return (
    <section className={styles.recipeView}>
      <Subheader backward headerText={values?.name} navigation="/recipes">
        <div className={styles.buttonBox}>
          {isRecipeOwner && (
            <>
              <BaseButton className={styles.removeRecipe} onClick={handleRemove}>
                <img src={removeIcon} alt="removeIcon" className={styles.removeIcon} />
              </BaseButton>
              <BaseLink
                primary
                linkText="Редактировать"
                className={styles.editBtn}
                navigation={`/recipes/edit/${recipeId}`}
              />
            </>
          )}
        </div>
      </Subheader>

      <BaseCard props={values} />
      <div className={styles.mainContainer}>
        <div className={styles.ingredientsContainer}>
          <h4 className={styles.ingredientsHeader}>Ингредиенты</h4>
          {values?.ingredients.map((ingredient, index) => (
            <div key={index} className={styles.ingredientBox}>
              <p className={styles.ingredientsName}>{ingredient.name}</p>
              <p className={styles.ingredientsText}>{ingredient.product}</p>
            </div>
          ))}
        </div>
        <div className={styles.stepsContainer}>
          {values?.steps.map((step, index) => (
            <div key={index} className={styles.stepBox}>
              <p className={styles.stepHeader}>Шаг {index + 1}</p>
              <div className={styles.stepTextBox}>
                <p className={styles.stepText}>{step.description}</p>
              </div>
            </div>
          ))}
          <h3 className={styles.stepsMealEnjoyText}>Приятного Аппетита!</h3>
        </div>
      </div>
    </section>
  );
};

export default RecipeView;
