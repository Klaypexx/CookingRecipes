import { Link } from 'react-router-dom';
import RecipesListProps from '../../../Types/RecipesListProps';
import BaseButton from '../../Button/BaseButton/BaseButton';
import BaseCard from '../../Card/BaseCard/BaseCard';
import styles from './RecipesList.module.css';

const RecipesList: React.FC<RecipesListProps> = ({ isLoadButton, handleClick, values }) => {
  return (
    <>
      <div className={styles.recipesContainer}>
        {values.length > 0 ? (
          <>
            {values.map((value, index) => (
              <Link key={index} to={`/recipes/${value.id}`}>
                <BaseCard props={value} recipeId={value.id.toString()} />
              </Link>
            ))}
          </>
        ) : (
          <div className={styles.noRecipesBox}>
            <h4 className={styles.noRecipeText}>Список рецептов пуст</h4>
          </div>
        )}
      </div>
      {isLoadButton && <BaseButton onClick={handleClick} buttonText="Загрузить еще" className={styles.loadButton} />}
    </>
  );
};

export default RecipesList;
