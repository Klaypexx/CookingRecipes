import { useEffect, useState } from 'react';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import SearchBlock from '../../Components/Search/SearchBlock';
import Subheader from '../../Components/Subheader/Subheader';
import TagsBlockList from '../../Components/Tags/TagsBlockList/TagsList';
import styles from './RecipeList.module.css';
import RecipeService from '../../Services/RecipeService';
import RecipeListValues from '../../Types/RecipeListValues';
import Spinner from '../../Components/Spinner/Spinner';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import { Link } from 'react-router-dom';
import useAuthStore from '../../Stores/useAuthStore';
import SearchBlockValues from '../../Types/SearchBlockValues';
import useSearchStore from '../../Stores/useSearchStore';

const RecipesList = () => {
  let [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [values, setValues] = useState<RecipeListValues[]>([]);
  const [isFirstMount, setIsFirstMount] = useState(true);
  const { searchString, setSearchString } = useSearchStore();
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    const fetchRecipes = async () => {
      const result = await RecipeService.GetRecipes(pageNumber, searchString);
      if (result.response && result.response.status === 200) {
        if (!result.response.data.length) {
          setIsLoadButton(false);
        }
        setValues((prevValues) => [...prevValues, ...result.response.data]);
        setLoading(false);
      }
    };
    fetchRecipes();
  }, [pageNumber]);

  useEffect(() => {
    if (isFirstMount) {
      setIsFirstMount(false);
      return;
    }
    const fetchRecipes = async () => {
      setLoading(true);
      const result = await RecipeService.GetRecipes(1, searchString);
      if (result.response && result.response.status === 200) {
        if (!result.response.data.length) {
          setIsLoadButton(false);
        }
        setValues(() => [...result.response.data]);
        setLoading(false);
      }
    };
    fetchRecipes();
  }, [isAuthorized, searchString]);

  const handleSubmit = async (values: SearchBlockValues) => {
    const result = await RecipeService.GetRecipes(pageNumber, values.searchString);
    if (result.response && result.response.status === 200) {
      if (!result.response.data.length) {
        setIsLoadButton(false);
      }
      setValues(() => [...result.response.data]);
      setSearchString(values.searchString);
    }
  };

  const handleTagsBlockClick = (value: string) => {
    setSearchString(value);
  };

  const handleClick = () => {
    setPageNumber((pageNumber) => pageNumber + 1);
  };

  if (loading) {
    return <Spinner />;
  }

  return (
    <div className={styles.recipesList}>
      <section>
        <Subheader text="Рецепты">
          <BaseLink base primary to="/recipes/create" text="Добавить рецепт" />
        </Subheader>

        <div className={styles.tagListContainer}>
          <TagsBlockList className={styles.tagList} onClick={handleTagsBlockClick} />
        </div>
      </section>

      <section>
        <div className={styles.searchContainer}>
          <SearchBlock text onSubmit={handleSubmit} />
        </div>

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
      </section>
    </div>
  );
};

export default RecipesList;
