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
    const fetchRecipes = async () => {
      await RecipeService.GetRecipes(pageNumber, searchString).then((res) => {
        if (res) {
          if (!res.response.data.length) {
            setIsLoadButton(false);
          }
          setValues((prevValues) => [...prevValues, ...res.response.data]);
          setLoading(false);
        }
      });
    };
    fetchRecipes();
  }, [pageNumber, searchString, isAuthorized]);

  const handleSubmit = async (value: SearchBlockValues) => {
    if (searchString == value.searchString) {
      return;
    }

    setValues([]);
    setPageNumber(1);
    setSearchString(value.searchString);
  };

  const handleClick = () => {
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
          <TagsBlockList className={styles.tagList} />
        </div>
      </section>

      <section>
        <div className={styles.searchContainer}>
          <SearchBlock text onSubmit={handleSubmit} />
        </div>
      </section>

      <section className={styles.recipesListSection}>
        {loading ? (
          <Spinner />
        ) : (
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
            {isLoadButton && (
              <BaseButton onClick={handleClick} buttonText="Загрузить еще" className={styles.loadButton} />
            )}
          </>
        )}
      </section>
    </div>
  );
};

export default RecipesList;
