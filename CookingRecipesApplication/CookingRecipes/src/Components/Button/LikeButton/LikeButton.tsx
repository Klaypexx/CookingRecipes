import BaseButton from '../BaseButton/BaseButton';
import like from '../../../resources/icons/like.svg';
import likeActive from '../../../resources/icons/likeActive.svg';
import styles from './LikeButton.module.css';
import { useEffect, useState } from 'react';
import LikeButtonProps from '../../../Types/LikeButtonProps';

const LikeButton: React.FC<LikeButtonProps> = ({ isLikePressed, recipeId }) => {
  const [isLike, setIsLike] = useState<boolean>(false);
  let [loading, setLoading] = useState(true);

  useEffect(() => {
    setIsLike(isLikePressed);
    setLoading(false);
  });

  if (loading) {
    return undefined;
  }

  const handleChangeLike = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();
    setIsLike((isLike) => !isLike);
  };

  return (
    <BaseButton className={styles.like} buttonText="12" onClick={handleChangeLike}>
      <img src={isLike ? likeActive : like} alt="like" className={styles.likeImage} />
    </BaseButton>
  );
};

export default LikeButton;
