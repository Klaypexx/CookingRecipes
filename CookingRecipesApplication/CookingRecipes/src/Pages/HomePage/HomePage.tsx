import { Link, useNavigate } from 'react-router-dom';
import CustomCard from '../../Components/Card/CustomCard/CustomCard';
import Preview from '../../Components/Preview/Preview';
import SearchBlock from '../../Components/Search/SearchBlock';
import TagsBlockList from '../../Components/Tags/TagsBlockList/TagsList';
import useSearchStore from '../../Stores/useSearchStore';
import SearchBlockValues from '../../Types/SearchBlockValues';
import styles from './HomePage.module.css';
import { useEffect, useState } from 'react';
import RecipeService from '../../Services/RecipeService';
import HomePageRecipeValues from '../../Types/HomePageRecipeValues';
import Spinner from '../../Components/Spinner/Spinner';

const HomePage = () => {
  let [loading, setLoading] = useState(true);
  const { setSearchString } = useSearchStore();
  const [values, setValues] = useState<HomePageRecipeValues>();
  const navigation = useNavigate();

  useEffect(() => {
    const fetchRecipe = async () => {
      await RecipeService.GetMostLikedRecipe().then((res) => {
        if (res) {
          setValues(() => res.response.data);
          setLoading(false);
          console.log(res.response.data);
        }
      });
    };
    fetchRecipe();
  }, []);

  const handleSubmit = (values: SearchBlockValues) => {
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
        <TagsBlockList text className={styles.tagList} />
      </section>

      {loading ? (
        <Spinner />
      ) : (
        <section className={styles.customCardSection}>
          <Link to={`/recipes/${values!.id}`}>
            <CustomCard props={values!} />
          </Link>
        </section>
      )}

      <section className={styles.searchSection}>
        <div className={styles.searchTextBox}>
          <h2 className={styles.searchHeader}>Поиск рецептов</h2>
          <p>Введите примерное название блюда, а мы по тегам найдем его</p>
        </div>
        <SearchBlock onSubmit={handleSubmit} />
      </section>
    </>
  );
};

export default HomePage;
