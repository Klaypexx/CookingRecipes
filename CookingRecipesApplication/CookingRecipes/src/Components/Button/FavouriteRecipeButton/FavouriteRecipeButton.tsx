import { useEffect, useState } from 'react';
import useAuthStore from '../../../Stores/useAuthStore';
import FavouriteRecipeService from '../../../Services/FavouriteRecipeService';
import BaseButton from '../BaseButton/BaseButton';
import FavouriteRecipeButtonProps from '../../../Types/FavouriteRecipeButtonProps';
import favourite from '../../../resources/icons/favourite.svg';
import favouriteActive from '../../../resources/icons/favouriteActive.svg';
import styles from './FavouriteRecipeButton.module.css';

const FavouriteRecipeButton: React.FC<FavouriteRecipeButtonProps> = ({
  isFavouritePressed,
  favouriteRecipeCount,
  recipeId,
}) => {
  const [isFavourite, setIsFavourite] = useState<boolean>(false);
  const [count, setCount] = useState<number>(0);
  let [loading, setLoading] = useState(true);
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    setLoading(false);
    setIsFavourite(isFavouritePressed);
    setCount(favouriteRecipeCount);
  }, []);

  const handleChangeFavourite = async (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();
    if (!isAuthorized) {
      return;
    }
    if (isFavourite) {
      await FavouriteRecipeService.removeFavouriteRecipe(recipeId).then(() => {
        setCount((count) => count - 1);
        setIsFavourite(false);
      });
    } else {
      await FavouriteRecipeService.addFavouriteRecipe(recipeId).then(() => {
        setCount((count) => count + 1);
        setIsFavourite(true);
      });
    }
  };

  if (loading) {
    return;
  }

  return (
    <BaseButton className={styles.favourite} buttonText={count.toString()} onClick={handleChangeFavourite}>
      <img src={isFavourite ? favouriteActive : favourite} alt="favouriteImage" className={styles.favouriteImage} />
    </BaseButton>
  );
};

export default FavouriteRecipeButton;
