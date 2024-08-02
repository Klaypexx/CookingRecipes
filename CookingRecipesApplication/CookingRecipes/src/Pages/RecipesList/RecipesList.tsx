import { useEffect, useState } from 'react';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import SearchBlock from '../../Components/Search/SearchBlock';
import Subheader from '../../Components/Subheader/Subheader';
import TagsBlockList from '../../Components/Tags/TagsBlockList/TagsList';
import styles from './RecipeList.module.css';
import RecipeService from '../../Services/RecipeService';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import { NavLink } from 'react-router-dom';
import RecipeListValues from '../../Types/RecipeListValues';

const RecipesList = () => {
  const [page, setPage] = useState(1);
  const [isLoad, setIsLoad] = useState(true);
  const [values, setValues] = useState<RecipeListValues[]>([]);

  useEffect(() => {
    const fetchRecipes = async () => {
      const result = await RecipeService.getAllRecipes(page);
      if (result.response && result.response.status === 200) {
        if (result.response.data.length < 4) {
          setIsLoad(false);
        }
        setValues((prevValues) => [...prevValues, ...result.response.data]);
      } else {
        throw Error(result.message);
      }
    };
    fetchRecipes();
  }, [page]);

  const handleSubmit = () => {
    return;
  };

  const handleClick = () => {
    setPage((page) => page + 1);
  };

  return (
    <section className={styles.recipesListSection}>
      <Subheader headerText="Рецепты">
        <BaseLink base primary navigation="/recipes/create" linkText="Добавить рецепт" />
      </Subheader>
      <div className={styles.tagListBlock}>
        <TagsBlockList className={styles.tagList} />
      </div>
      <div className={styles.searchBlock}>
        <SearchBlock text onSubmit={handleSubmit} />
      </div>
      <div className={styles.recipesListBlock}>
        {values.map((value, index) => (
          <NavLink key={index} to={`/recipes/${value.id}`} state={{ from: location.pathname }}>
            <BaseCard props={value} />
          </NavLink>
        ))}
      </div>
      {isLoad ? (
        <BaseButton onClick={handleClick} buttonText="Загрузить еще" className={styles.loadButton} />
      ) : undefined}
    </section>
  );
};

export default RecipesList;
