import classNames from 'classnames';
import styles from './BaseCard.module.css';
import timeIcon from '../../../resources/icons/time.svg';
import personIcon from '../../../resources/icons/person.svg';
import { IMAGE_URL } from '../../../Constants/httpUrl';
import CardProps from '../../../Types/CardProps';
import LikeButton from '../../Button/LikeButton/LikeButton';
import FavouriteRecipeButton from '../../Button/FavouriteRecipeButton/FavouriteRecipeButton';

const BaseCard: React.FC<CardProps> = ({ className, props, recipeId }) => {
  return (
    <div className={classNames(styles.cardContainer, className)}>
      <div className={styles.avatarImageBox}>
        <div className={styles.authorRecipeBox}>
          <p className={styles.authorRecipeText}>{`@${props?.authorName}`}</p>
        </div>
        {props?.avatarPath ? (
          <img src={IMAGE_URL + props.avatarPath} alt="avatarImage" className={styles.avatarImage} />
        ) : (
          <div className={styles.avatarNoImage}></div>
        )}
      </div>
      <div className={styles.cardInformationBox}>
        {props?.tags && (
          <div className={styles.cardHeaderBox}>
            <div className={styles.tagsFlex}>
              {props.tags.map((tag, index) => (
                <div key={index} className={styles.tagBox}>
                  <p className={styles.tagText}>{tag.name}</p>
                </div>
              ))}
            </div>
            <div className={styles.interactiveBox}>
              <FavouriteRecipeButton
                isFavouritePressed={props.isFavourite}
                favouriteRecipeCount={props.favouriteCount}
                recipeId={recipeId}
              />
              <LikeButton isLikePressed={props.isLike} likeCount={props.likeCount} recipeId={recipeId} />
            </div>
          </div>
        )}
        {props?.description && props.cookingTime && props.portion && (
          <>
            <div className={styles.gridBox}>
              <div className={styles.cardInfoBox}>
                {props.name && <h3 className={styles.cardInfoHeader}>{props.name}</h3>}
                <div className={styles.cardInfoTextBox}>
                  <p className={styles.cardInfoText}>{props?.description}</p>
                </div>
              </div>
              <div className={styles.cardNumerableFlex}>
                <div className={styles.cardNumerableBox}>
                  <img src={timeIcon} alt="time-icon" className={styles.timeIcon} />
                  <div className={styles.cardNumerableTextBox}>
                    <p className={styles.cardNumerableHeader}>Время приготовления:</p>
                    <p className={styles.cardNumerableTime}>{props?.cookingTime} мин</p>
                  </div>
                </div>
                <div className={styles.cardNumerableBox}>
                  <img src={personIcon} alt="person-icon" className={styles.personIcon} />
                  <div className={styles.cardNumerableTextBox}>
                    <p className={styles.cardNumerableHeader}>Рецепт на:</p>
                    <p className={styles.cardNumerableTime}>{props?.portion} персон</p>
                  </div>
                </div>
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default BaseCard;
