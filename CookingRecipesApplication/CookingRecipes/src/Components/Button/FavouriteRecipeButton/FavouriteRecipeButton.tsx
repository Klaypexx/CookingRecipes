import { useEffect, useState } from 'react';
import FavouriteRecipeService from '../../../Services/FavouriteRecipeService';
import useAuthStore from '../../../Stores/useAuthStore';
import useUserStore from '../../../Stores/useUserStore';
import FavouriteRecipeButtonProps from '../../../Types/FavouriteRecipeButtonProps';
import favourite from '../../../resources/icons/favourite.svg';
import favouriteActive from '../../../resources/icons/favouriteActive.svg';
import BaseButton from '../BaseButton/BaseButton';
import styles from './FavouriteRecipeButton.module.css';

const FavouriteRecipeButton: React.FC<FavouriteRecipeButtonProps> = ({
  isFavouritePressed,
  favouriteRecipeCount,
  recipeId,
}) => {
  const [isFavourite, setIsFavourite] = useState<boolean>(false);
  const [count, setCount] = useState<number>(0);
  const { setFavouritesCount } = useUserStore();
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
        setFavouritesCount(-1);
        setIsFavourite(false);
      });
    } else {
      await FavouriteRecipeService.addFavouriteRecipe(recipeId).then(() => {
        setCount((count) => count + 1);
        setFavouritesCount(1);
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
