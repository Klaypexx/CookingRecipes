import { useEffect, useState } from 'react';
import like from '../../../resources/icons/like.svg';
import likeActive from '../../../resources/icons/likeActive.svg';
import LikeService from '../../../Services/LikeService';
import useAuthStore from '../../../Stores/useAuthStore';
import useUserStore from '../../../Stores/useUserStore';
import LikeButtonProps from '../../../Types/LikeButtonProps';
import BaseButton from '../BaseButton/BaseButton';
import styles from './LikeButton.module.css';

const LikeButton: React.FC<LikeButtonProps> = ({ isLikePressed, likeCount, recipeId }) => {
  const [isLike, setIsLike] = useState<boolean>(false);
  const [count, setCount] = useState<number>(0);
  const { setLikesCount } = useUserStore();
  let [loading, setLoading] = useState(true);
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
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
        setLikesCount(-1);
        setIsLike(false);
      });
    } else {
      await LikeService.addLike(recipeId).then(() => {
        setCount((count) => count + 1);
        setLikesCount(1);
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
