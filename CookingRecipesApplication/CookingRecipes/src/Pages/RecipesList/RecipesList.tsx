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

const RecipesList = () => {
  let [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [values, setValues] = useState<RecipeListValues[]>([]);

  useEffect(() => {
    const fetchRecipes = async () => {
      const result = await RecipeService.GetRecipesForPage(pageNumber);
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

  const handleSubmit = () => {
    return;
  };

  const handleClick = () => {
    setPageNumber((pageNumber) => pageNumber + 1);
  };

  if (loading) {
    return <Spinner />;
  }

  return (
    <section className={styles.recipesListSection}>
      <Subheader headerText="Рецепты">
        <BaseLink base primary to="/recipes/create" linkText="Добавить рецепт" />
      </Subheader>
      <div className={styles.tagListBlock}>
        <TagsBlockList className={styles.tagList} />
      </div>
      {values.length > 0 && (
        <div className={styles.searchBlock}>
          <SearchBlock text onSubmit={handleSubmit} />
        </div>
      )}

      <div className={styles.recipesListBlock}>
        {values.length > 0 ? (
          <>
            {values.map((value, index) => (
              <Link key={index} to={`/recipes/${value.id}`}>
                <BaseCard props={value} />
              </Link>
            ))}
          </>
        ) : (
          <div className={styles.noRecipesBlock}>
            <h4 className={styles.noRecipeText}>Список рецептов пуст</h4>
          </div>
        )}
      </div>
      {isLoadButton && <BaseButton onClick={handleClick} buttonText="Загрузить еще" className={styles.loadButton} />}
    </section>
  );
};

export default RecipesList;
