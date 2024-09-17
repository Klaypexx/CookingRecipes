import RecipeViewProps from '../../../Types/RecipeViewProps';
import BaseCard from '../../Card/BaseCard/BaseCard';
import styles from './RecipeView.module.css';

const RecipeView: React.FC<RecipeViewProps> = ({ recipeId, values }) => {
  return (
    <div className={styles.recipeView}>
      <BaseCard props={values} recipeId={recipeId} />
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
    </div>
  );
};

export default RecipeView;
