import bookIcon from '../../../resources/icons/ic-menu.svg';
import BaseStatisticBlockProps from '../../../Types/BaseStatisticBlockProps';
import styles from './BaseStatisticBlock.module.css';

const BaseStatisticBlock: React.FC<BaseStatisticBlockProps> = ({ name, count }) => {
  return (
    <div className={styles.statisticBox}>
      <div className={styles.leftInfo}>
        <div className={styles.iconBox}>
          <img src={bookIcon} alt="bookIcon" className={styles.bookIcon} />
        </div>
        <p className={styles.statisticText}>{name}</p>
      </div>
      <div className={styles.rightInfo}>
        <p className={styles.statisticCountText}>{count}</p>
      </div>
    </div>
  );
};

export default BaseStatisticBlock;
