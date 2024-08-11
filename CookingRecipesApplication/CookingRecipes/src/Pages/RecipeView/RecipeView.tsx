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

const RecipeView = () => {
  const [loading, setLoading] = useState(true);
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
      } else {
        throw Error(result.message);
      }
    };
    fetchRecipes();
  }, []);

  const handleRemove = async () => {
    const result = await RecipeService.removeRecipe(recipeId!);

    if (result.response && result.response.status === 200) {
      successToast('Рецепт успешно удален');
      navigate(location.state?.from);
    } else {
      throw Error(result.message);
    }
  };

  return (
    <section className={styles.recipeView}>
      <Subheader backward headerText={loading ? undefined : values?.name} navigation="/recipes">
        <div className={styles.buttonBox}>
          {loading ? undefined : (
            <>
              <button className={styles.removeRecipe} onClick={handleRemove}>
                <img src={removeIcon} alt="removeIcon" className={styles.removeIcon} />
              </button>
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
      {loading ? (
        <Spinner />
      ) : (
        <>
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
        </>
      )}
    </section>
  );
};

export default RecipeView;
