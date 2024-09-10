import { useEffect, useState } from 'react';
import RecipesListBlock from '../../Components/Recipe/RecipesList/RecipesList';
import Spinner from '../../Components/Spinner/Spinner';
import Subheader from '../../Components/Subheader/Subheader';
import RecipeService from '../../Services/RecipeService';
import FavouritesRecipeValues from '../../Types/FavouritesRecipeValues';
import styles from './Favourites.module.css';

const Favourites = () => {
  const [loading, setLoading] = useState(true);
  const [recipeValues, setRecipeValues] = useState<FavouritesRecipeValues[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);

  useEffect(() => {
    const fetchRecipes = async () => {
      await RecipeService.GetFavouriteRecipes(pageNumber).then((res) => {
        if (res) {
          setIsLoadButton(!res.response.data.isLastRecipes);
          setRecipeValues((prevValues) => [...prevValues, ...res.response.data.recipes]);
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
          <RecipesListBlock isLoadButton={isLoadButton} handleClick={() => handleClick()} values={recipeValues} />
        )}
      </section>
    </div>
  );
};

export default Favourites;
