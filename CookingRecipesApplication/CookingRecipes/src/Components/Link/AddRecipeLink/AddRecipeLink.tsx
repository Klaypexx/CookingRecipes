import BaseLink from '../BaseLink/BaseLink';
import plusImg from '../../../resources/icons/plus-white.svg';
import styles from './AddRecipeLink.module.css';

const AddRecipeLink = () => {
  return (
    <>
      <BaseLink primary navigation="/recipes/create" linkText="Добавить рецепт">
        <img src={plusImg} className={styles.plus} />
      </BaseLink>
    </>
  );
};

export default AddRecipeLink;
