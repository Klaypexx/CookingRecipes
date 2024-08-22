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
    setIsLike(isLikePressed);
    setLoading(false);
    setCount(likeCount);
  }, []);

  const handleChangeLike = async (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();
    if (!isAuthorized) {
      return;
    }
    if (isLike) {
      const result = await LikeService.removeLike(recipeId);
      if (result.response && result.response.status === 200) {
        setCount((count) => count - 1);
        setIsLike(false);
      }
    } else {
      const result = await LikeService.addLike(recipeId);
      if (result.response && result.response.status === 200) {
        setCount((count) => count + 1);
        setIsLike(true);
      }
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
