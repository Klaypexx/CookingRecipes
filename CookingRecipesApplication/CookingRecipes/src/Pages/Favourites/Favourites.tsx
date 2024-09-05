import { useEffect, useState } from 'react';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './Favourites.module.css';
import RecipeService from '../../Services/RecipeService';
import RecipesListBlock from '../../Components/Recipe/RecipesList/RecipesList';
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
          <RecipesListBlock isLoadButton={isLoadButton} handleClick={() => handleClick()} values={values} />
        )}
      </section>
    </div>
  );
};

export default Favourites;
