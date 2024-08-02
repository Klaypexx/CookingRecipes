import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './RecipeView.module.css';
import removeIcon from '../../resources/icons/remove.svg';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import { useEffect, useState } from 'react';
import { useParams, useRouteError } from 'react-router-dom';
import RecipeService from '../../Services/RecipeService';
import RecipeViewValues from '../../Types/RecipeViewValues';

const RecipeView = () => {
  const [values, setValues] = useState<RecipeViewValues>();
  const { recipeId } = useParams();

  useEffect(() => {
    window.scrollTo(0, 0); // Прокрутка к началу страницы
  }, []);

  useEffect(() => {
    const fetchRecipes = async () => {
      try {
        const response: RecipeViewValues = await RecipeService.getCurrentUserRecipe(recipeId!);
        setValues(response);
        console.log(response);
      } catch (error) {
        throw new Error('Failed to fetch recipe');
      }
    };
    fetchRecipes();
  }, []);

  return (
    <section className={styles.recipeView}>
      <Subheader backward headerText={values?.name}>
        <div className={styles.buttonBox}>
          <button className={styles.removeRecipe}>
            <img src={removeIcon} alt="removeIcon" className={styles.removeIcon} />
          </button>
          <BaseButton primary buttonText="Редактировать" className={styles.editBtn} />
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
              <p className={styles.stepHeader}>Шаг {index}</p>
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
