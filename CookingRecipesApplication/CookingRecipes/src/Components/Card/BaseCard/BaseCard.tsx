import classNames from 'classnames';
import styles from './BaseCard.module.css';
import { CardProps } from '../../../Types/types';
import { useEffect, useState } from 'react';
import timeIcon from '../../../resources/icons/time.svg';
import personIcon from '../../../resources/icons/person.svg';
import { IMAGE_URL } from '../../../Constants/httpUrl';

const BaseCard: React.FC<CardProps> = ({ margin, className, props }) => {
  const [image, setImage] = useState<string | undefined>(undefined);
  useEffect(() => {
    setImage(props.avatarPath);
  }, []);

  return (
    <div className={classNames(margin ? styles.margin : undefined, styles.cardContainer, className)}>
      <div className={styles.cardBackground}></div>
      <div className={styles.cardPhoto}>
        {image && <img src={IMAGE_URL + image} alt="avatar" className={styles.avatarImage} />}
      </div>
      <div className={styles.cardInformation}>
        <div className={styles.cardHeader}>
          <div className={styles.tagsContainer}>
            {props.tags.map((tag, index) => (
              <div key={index} className={styles.tagBox}>
                <p className={styles.tagText}>{tag.name}</p>
              </div>
            ))}
          </div>
        </div>
        {props?.description && props.cookingTime && props.portion ? (
          <>
            <div className={styles.cardInfoContainer}>
              <h3 className={styles.cardInfoHeader}>{props?.name}</h3>
              <div className={styles.cardInfoTextBox}>
                <p className={styles.cardInfoText}>{props?.description}</p>
              </div>
              <div className={styles.cardNumerableContainer}>
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
        ) : undefined}
      </div>
    </div>
  );
};

export default BaseCard;
