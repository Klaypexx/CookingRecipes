import BaseLink from '../../Components/Link/BaseLink/BaseLink';
import SearchBlock from '../../Components/Search/SearchBlock';
import Subheader from '../../Components/Subheader/Subheader';
import TagsBlockList from '../../Components/Tags/TagsBlockList/TagsList';
import styles from './RecipeList.module.css';

const RecipesList = () => {
  const handleSubmit = () => {
    return;
  };
  return (
    <>
      <section className={styles.recipesListSection}>
        <Subheader headerText="Рецепты">
          <BaseLink primary linkText="Добавить рецепт" />
        </Subheader>
        <div className={styles.tagListBlock}>
          <TagsBlockList className={styles.tagList} />
        </div>
        <div className={styles.searchBlock}>
          <SearchBlock text onSubmit={handleSubmit} />
        </div>
      </section>
    </>
  );
};

export default RecipesList;
