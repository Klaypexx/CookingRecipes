import { useEffect, useState } from 'react';
import SearchForm from '../../Components/Form/SearchForm/SearchForm';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import RecipesListBlock from '../../Components/Recipe/RecipesList/RecipesList';
import Spinner from '../../Components/Spinner/Spinner';
import Subheader from '../../Components/Subheader/Subheader';
import MiniTagsList from '../../Components/Tags/MiniTagsList/MiniTagsList';
import TagsList from '../../Components/Tags/TagsList/TagsList';
import RecipeService from '../../Services/RecipeService';
import useAuthStore from '../../Stores/useAuthStore';
import RecipeListValues from '../../Types/RecipeListValues';
import SearchBlockValues from '../../Types/SearchBlockValues';
import styles from './RecipeList.module.css';

const RecipesList = () => {
  const [loading, setLoading] = useState(true);
  const [recipeValues, setRecipeValues] = useState<RecipeListValues[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [isFirstMount, setIsFirstMount] = useState(true);
  const [searchString, setSearchString] = useState('');
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  //Для обновления состояния лайков и избранных после логинации
  useEffect(() => {
    if (isFirstMount) {
      setIsFirstMount(false);
      return;
    }
    setLoading(true);
    setSearchString('');
    setRecipeValues([]);
    setPageNumber(1);
  }, [isAuthorized]);

  useEffect(() => {
    const fetchRecipes = async () => {
      await RecipeService.GetRecipes(pageNumber, searchString).then((res) => {
        if (res) {
          setIsLoadButton(!res.response.data.isLastRecipes);
          setRecipeValues((prevValues) => [...prevValues, ...res.response.data.recipes]);
          setLoading(false);
        }
      });
    };
    fetchRecipes();
  }, [pageNumber, searchString, isAuthorized]);

  const handleSearchSubmit = async (value: SearchBlockValues) => {
    if (searchString == value.searchString) {
      return;
    }
    setRecipeValues([]);
    setPageNumber(1);
    setSearchString(value.searchString);
  };

  const handlePaginationClick = () => {
    setPageNumber((pageNumber) => pageNumber + 1);
  };

  return (
    <div className={styles.recipesList}>
      <section>
        <Subheader text="Рецепты">
          <BaseLink base primary to="/recipes/create" text="Добавить рецепт" />
        </Subheader>
      </section>

      <section>
        <div className={styles.tagListBox}>
          <TagsList className={styles.tagList} />
        </div>
      </section>

      <section>
        <div className={styles.searchBox}>
          <SearchForm text onSubmit={handleSearchSubmit} />
          <MiniTagsList />
        </div>
      </section>

      <section className={styles.recipesListSection}>
        {loading ? (
          <Spinner />
        ) : (
          <RecipesListBlock
            isLoadButton={isLoadButton}
            handleClick={() => handlePaginationClick()}
            values={recipeValues}
          />
        )}
      </section>
    </div>
  );
};

export default RecipesList;
