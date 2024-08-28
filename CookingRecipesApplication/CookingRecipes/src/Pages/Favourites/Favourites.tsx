import { useEffect, useState } from 'react';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './Favourites.module.css';
import RecipeService from '../../Services/RecipeService';
import { Link } from 'react-router-dom';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import Spinner from '../../Components/Spinner/Spinner';
import FavouritesRecipeValues from '../../Types/FavouritesRecipeValues';

const Favourites = () => {
  let [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [values, setValues] = useState<FavouritesRecipeValues[]>([]);

  useEffect(() => {
    const fetchRecipes = async () => {
      await RecipeService.GetFavouriteRecipes(pageNumber).then((res) => {
        if (res) {
          setIsLoadButton(!res.response.data.isLastRecipes);
          setValues((prevValues) => [...prevValues, ...res.response.data.recipes]);
          setLoading(false);
        }
      });
    };
    fetchRecipes();
  }, [pageNumber]);

  const handleClick = () => {
    setPageNumber((pageNumber) => pageNumber + 1);
  };

  return (
    <div className={styles.favourites}>
      <section>
        <Subheader text="Избранное" />
      </section>
      <section className={styles.recipesListSection}>
        {loading ? (
          <Spinner />
        ) : (
          <>
            <div className={styles.recipesListBlock}>
              {values.length > 0 ? (
                <>
                  {values.map((value, index) => (
                    <Link key={index} to={`/recipes/${value.id}`}>
                      <BaseCard props={value} recipeId={value.id.toString()} />
                    </Link>
                  ))}
                </>
              ) : (
                <div className={styles.noRecipesBlock}>
                  <h4 className={styles.noRecipeText}>Ваш список пуст</h4>
                </div>
              )}
            </div>
            {isLoadButton && (
              <BaseButton onClick={handleClick} buttonText="Загрузить еще" className={styles.loadButton} />
            )}
          </>
        )}
      </section>
    </div>
  );
};

export default Favourites;
