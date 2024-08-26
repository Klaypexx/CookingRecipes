import styles from './CustomCard.module.css';
import { IMAGE_URL } from '../../../Constants/httpUrl';
import CustomCardProps from '../../../Types/CustomCardProps';
import like from '../../../resources/icons/customLike.svg';
import timer from '../../../resources/icons/customTimer.svg';
import yummyIcon from '../../../resources/icons/yummy.svg';

const CustomCard: React.FC<CustomCardProps> = ({ props }) => {
  return (
    <div className={styles.cardContainer}>
      <div className={styles.avatarImageBox}>
        <div className={styles.authorRecipeBox}>
          <p className={styles.authorRecipeText}>{`@${props.authorName}`}</p>
        </div>
        {props?.avatarPath ? (
          <img src={IMAGE_URL + props.avatarPath} alt="avatarImage" className={styles.avatarImage} />
        ) : (
          <div className={styles.avatarNoImage}></div>
        )}
      </div>
      <div className={styles.cardInformationBox}>
        <div className={styles.numerableBox}>
          <div className={styles.flexContent}>
            <img src={like} alt="like" className={styles.numerableIcon} />
            <p className={styles.numerableText}>{props.likeCount}</p>
          </div>
          <div className={styles.flexContent}>
            <img src={timer} alt="timer" className={styles.numerableIcon} />
            <p className={styles.numerableText}>{props.cookingTime} минут</p>
          </div>
        </div>
        <div className={styles.yummyBox}>
          <img src={yummyIcon} alt="yummyIcon" className={styles.yummyIcon} />
        </div>
        <div className={styles.textBox}>
          <h2 className={styles.headerText}>{props.name}</h2>
          <p className={styles.text}>{props.description}</p>
        </div>
      </div>
    </div>
  );
};

export default CustomCard;
