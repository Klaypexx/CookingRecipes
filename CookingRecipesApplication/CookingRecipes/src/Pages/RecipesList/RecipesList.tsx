import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import SearchForm from '../../Components/Form/SearchForm/SearchForm';
import RecipesListBlock from '../../Components/Recipe/RecipesList/RecipesList';
import Spinner from '../../Components/Spinner/Spinner';
import Subheader from '../../Components/Subheader/Subheader';
import MiniTagsList from '../../Components/Tags/MiniTagsList/MiniTagsList';
import TagsList from '../../Components/Tags/TagsList/TagsList';
import { warnToast } from '../../Components/Toast/Toast';
import RecipeService from '../../Services/RecipeService';
import TagService from '../../Services/TagService';
import useAuthStore from '../../Stores/useAuthStore';
import useModalStore from '../../Stores/useModalStore';
import RecipeListValues from '../../Types/RecipeListValues';
import SearchBlockValues from '../../Types/SearchBlockValues';
import styles from './RecipeList.module.css';

const RecipesList = () => {
  const [loading, setLoading] = useState(true);
  const [recipeValues, setRecipeValues] = useState<RecipeListValues[]>([]);
  const [tagsValues, setTagsValues] = useState<string[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [isFirstMount, setIsFirstMount] = useState(true);
  const [searchString, setSearchString] = useState('');
  const { isAuth, setAuth } = useModalStore();
  const { isAuthorized } = useAuthStore();
  const navigation = useNavigate();

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
  }, [pageNumber, isAuthorized]);

  useEffect(() => {
    const fetchTags = async () => {
      await TagService.getRandomTags().then((res) => {
        if (res) {
          setTagsValues(res.response.data);
        }
      });
    };
    fetchTags();
  }, []);

  const handleSearchSubmit = async (value: SearchBlockValues) => {
    if (searchString == value.searchString) {
      return;
    }
    setPageNumber(1);
    setSearchString(value.searchString);
    await RecipeService.GetRecipes(1, value.searchString).then((res) => {
      if (res) {
        setIsLoadButton(!res.response.data.isLastRecipes);
        setRecipeValues(() => [...res.response.data.recipes]);
      }
    });
  };

  const handlePaginationClick = () => {
    setPageNumber((pageNumber) => pageNumber + 1);
  };

  const onButtonClick = () => {
    if (!isAuthorized) {
      warnToast('Вы не вошли в систему');
      setAuth(isAuth);
      return;
    }
    navigation('/recipes/create');
  };

  return (
    <div className={styles.recipesList}>
      <section>
        <Subheader text="Рецепты">
          <BaseButton primary buttonText="Добавить рецепт" onClick={onButtonClick} />
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
          <MiniTagsList values={tagsValues} />
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
