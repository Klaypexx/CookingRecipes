import { useEffect, useState } from 'react';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import RecipesListBlock from '../../Components/Recipe/RecipesList/RecipesList';
import SearchBlock from '../../Components/Search/SearchBlock';
import Spinner from '../../Components/Spinner/Spinner';
import Subheader from '../../Components/Subheader/Subheader';
import TagsList from '../../Components/Tags/TagsList/TagsList';
import RecipeService from '../../Services/RecipeService';
import useAuthStore from '../../Stores/useAuthStore';
import RecipeListValues from '../../Types/RecipeListValues';
import SearchBlockValues from '../../Types/SearchBlockValues';
import styles from './RecipeList.module.css';

const RecipesList = () => {
  let [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [values, setValues] = useState<RecipeListValues[]>([]);
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
    setValues([]);
    setPageNumber(1);
  }, [isAuthorized]);

  useEffect(() => {
    console.log('Фетчинг');
    const fetchRecipes = async () => {
      await RecipeService.GetRecipes(pageNumber, searchString).then((res) => {
        if (res) {
          setIsLoadButton(!res.response.data.isLastRecipes);
          setValues((prevValues) => [...prevValues, ...res.response.data.recipes]);
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
    setValues([]);
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
        <div className={styles.tagListContainer}>
          <TagsList className={styles.tagList} />
        </div>
      </section>

      <section>
        <div className={styles.searchContainer}>
          <SearchBlock text onSubmit={handleSearchSubmit} />
        </div>
      </section>

      <section className={styles.recipesListSection}>
        {loading ? (
          <Spinner />
        ) : (
          <RecipesListBlock isLoadButton={isLoadButton} handleClick={() => handlePaginationClick()} values={values} />
        )}
      </section>
    </div>
  );
};

export default RecipesList;
