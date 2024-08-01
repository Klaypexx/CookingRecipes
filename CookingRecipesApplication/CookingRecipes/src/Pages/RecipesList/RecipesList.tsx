import { useEffect, useState } from 'react';
import BaseCard from '../../Components/Card/BaseCard/BaseCard';
import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import SearchBlock from '../../Components/Search/SearchBlock';
import Subheader from '../../Components/Subheader/Subheader';
import TagsBlockList from '../../Components/Tags/TagsBlockList/TagsList';
import styles from './RecipeList.module.css';
import RecipeService from '../../Services/RecipeService';
import BaseButton from '../../Components/Button/BaseButton/BaseButton';
import { RecipeListValues } from '../../Types/types';

const RecipesList = () => {
  const [page, setPage] = useState(1);
  const [isLoad, setIsLoad] = useState(true);
  const [values, setValues] = useState<RecipeListValues[]>([]);

  useEffect(() => {
    const fetchRecipes = async () => {
      try {
        const response: RecipeListValues[] = await RecipeService.getAllRecipes(page);
        if (response.length < 4) {
          setIsLoad(false);
        }
        setValues((prevValues) => [...prevValues, ...response]);
      } catch (error) {
        console.error('Error fetching recipes:', error);
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
    <>
      <section className={styles.recipesListSection}>
        <Subheader headerText="Рецепты">
          <BaseLink primary navigation="/recipe/create" linkText="Добавить рецепт" />
        </Subheader>
        <div className={styles.tagListBlock}>
          <TagsBlockList className={styles.tagList} />
        </div>
        <div className={styles.searchBlock}>
          <SearchBlock text onSubmit={handleSubmit} />
        </div>
        <div className={styles.recipesListBlock}>
          {values.map((value) => (
            <BaseCard key={value.idRecipe} props={value} />
          ))}
        </div>
        {isLoad ? (
          <BaseButton onClick={handleClick} buttonText="Загрузить еще" className={styles.loadButton} />
        ) : undefined}
      </section>
    </>
  );
};

export default RecipesList;
