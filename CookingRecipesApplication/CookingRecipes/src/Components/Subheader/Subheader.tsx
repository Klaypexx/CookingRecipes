import classNames from 'classnames';
import styles from './Subheader.module.css';
import arrow from '../../resources/icons/backward-arrow.svg';
import { Link, useLocation } from 'react-router-dom';
import SubheaderProps from '../../Types/SubheaderProps';

const Subheader: React.FC<SubheaderProps> = ({ backward, headerText, navigation, children }) => {
  const location = useLocation();

  const classList = classNames(backward ? styles.backward : undefined, styles.subheaderContainer);

  return (
    <div className={classList}>
      {backward && (
        <div className={styles.backwardContainer}>
          <Link
            to={navigation ? navigation : location.state?.from}
            state={{ from: location.pathname }}
            className={styles.backwardBox}
          >
            <img src={arrow} alt="backward arrow" className={styles.arrow} />
            <p className={styles.backwardText}>Назад</p>
          </Link>
        </div>
      )}
      <div className={styles.subheadBox}>
        <h2>{headerText}</h2>
        {children && <>{children}</>}
      </div>
    </div>
  );
};

export default Subheader;
