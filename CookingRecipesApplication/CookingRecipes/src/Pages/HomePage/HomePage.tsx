import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import CustomCard from '../../Components/Card/CustomCard/CustomCard';
import SearchForm from '../../Components/Form/SearchForm/SearchForm';
import Preview from '../../Components/Preview/Preview';
import Spinner from '../../Components/Spinner/Spinner';
import MiniTagsList from '../../Components/Tags/MiniTagsList/MiniTagsList';
import TagsList from '../../Components/Tags/TagsList/TagsList';
import RecipeService from '../../Services/RecipeService';
import TagService from '../../Services/TagService';
import useSearchStore from '../../Stores/useSearchStore';
import HomePageRecipeValues from '../../Types/HomePageRecipeValues';
import SearchBlockValues from '../../Types/SearchBlockValues';
import styles from './HomePage.module.css';

const HomePage = () => {
  const [loading, setLoading] = useState(true);
  const [recipeValues, setRecipeVaues] = useState<HomePageRecipeValues | null>();
  const [tagsValue, setTagsValue] = useState<string[]>([]);
  const { setSearchString } = useSearchStore();
  const navigation = useNavigate();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      await Promise.all([fetchRecipe(), fetchTags()]);
      setLoading(false);
    };
    fetchData();
  }, []);

  const fetchRecipe = async () => {
    await RecipeService.GetMostLikedRecipe().then((res) => {
      if (res) {
        setRecipeVaues(() => res.response.data);
      }
    });
  };

  const fetchTags = async () => {
    await TagService.getRandomTags().then((res) => {
      if (res) {
        setTagsValue(() => res.response.data);
      }
    });
  };

  const handleSearchSubmit = (values: SearchBlockValues) => {
    setSearchString(values.searchString);
    navigation('/recipes');
  };

  return (
    <>
      <section>
        <Preview />
      </section>

      <section className={styles.tagsSection}>
        <div className={styles.tagTextBox}>
          <h2>Умная сортировка по тегам</h2>
          <p className={styles.tagText}>
            Добавляй рецепты и указывай наиболее популярные теги. Это позволит быстро находить любые категории.
          </p>
        </div>
        <TagsList text className={styles.tagList} />
      </section>

      <section className={styles.customCardSection}>
        {loading ? (
          <Spinner />
        ) : (
          <>
            {recipeValues && (
              <Link to={`/recipes/${recipeValues!.id}`}>
                <CustomCard props={recipeValues!} />
              </Link>
            )}
          </>
        )}
      </section>

      <section className={styles.searchSection}>
        <div className={styles.searchTextBox}>
          <h2 className={styles.searchHeader}>Поиск рецептов</h2>
          <p>Введите примерное название блюда, а мы по тегам найдем его</p>
        </div>
        <div className={styles.searchBox}>
          <SearchForm onSubmit={handleSearchSubmit} />
          <MiniTagsList className={styles.miniTags} values={tagsValue!} />
        </div>
      </section>
    </>
  );
};

export default HomePage;
