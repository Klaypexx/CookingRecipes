import classNames from 'classnames';
import styles from './BaseCard.module.css';
import { CardProps } from '../../../Types/types';

const BaseCard: React.FC<CardProps> = ({ margin, className, children }) => {
  return (
    <>
      <div className={classNames(margin ? styles.margin : undefined, styles.cardContainer, className)}>
        <div className={styles.cardBackground}></div>
        <div className={styles.cardPhoto}></div>
        <div className={styles.cardInformation}>{children}</div>
      </div>
    </>
  );
};

export default BaseCard;
