import BaseCard from '../../Card/BaseCard/BaseCard';
import styles from './CardSkeleton.module.css';

const CardSkeleton = () => {
  return (
    <div className={styles.skeletonRecipeBox}>
      <BaseCard recipeId="1" />
    </div>
  );
};

export default CardSkeleton;
