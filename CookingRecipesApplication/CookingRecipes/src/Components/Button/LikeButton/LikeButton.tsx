import BaseButton from '../BaseButton/BaseButton';
import like from '../../../resources/icons/like.svg';
import likeActive from '../../../resources/icons/likeActive.svg';
import styles from './LikeButton.module.css';
import { useEffect, useState } from 'react';
import LikeButtonProps from '../../../Types/LikeButtonProps';
import LikeService from '../../../Services/LikeService';
import useAuthStore from '../../../Stores/useAuthStore';

const LikeButton: React.FC<LikeButtonProps> = ({ isLikePressed, likeCount, recipeId }) => {
  const [isLike, setIsLike] = useState<boolean>(false);
  const [count, setCount] = useState<number>(0);
  let [loading, setLoading] = useState(true);
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    console.log(`${isLikePressed}`);
    setLoading(false);
    setIsLike(isLikePressed);
    setCount(likeCount);
  }, []);

  const handleChangeLike = async (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();
    if (!isAuthorized) {
      return;
    }
    if (isLike) {
      await LikeService.removeLike(recipeId).then(() => {
        setCount((count) => count - 1);
        setIsLike(false);
      });
    } else {
      await LikeService.addLike(recipeId).then(() => {
        setCount((count) => count + 1);
        setIsLike(true);
      });
    }
  };

  if (loading) {
    return;
  }

  return (
    <BaseButton className={styles.like} buttonText={count.toString()} onClick={handleChangeLike}>
      <img src={isLike ? likeActive : like} alt="like" className={styles.likeImage} />
    </BaseButton>
  );
};

export default LikeButton;
