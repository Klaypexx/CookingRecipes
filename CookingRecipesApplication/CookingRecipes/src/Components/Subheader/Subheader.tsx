import classNames from 'classnames';
import styles from './Subheader.module.css';
import arrow from '../../resources/icons/backward-arrow.svg';
import { NavLink, useLocation } from 'react-router-dom';
import { SubheaderProps } from '../../Types/types';
import { useEffect } from 'react';

const Subheader: React.FC<SubheaderProps> = ({ backward, headerText, children }) => {
  const location = useLocation();

  useEffect(() => {
    console.log(location.state?.from);
  });

  const classList = classNames(backward ? styles.backward : undefined, styles.subheaderContainer);

  return (
    <div className={classList}>
      {backward && (
        <div className={styles.backwardContainer}>
          <NavLink to={location.state?.from} state={{ from: location.pathname }} className={styles.backwardBox}>
            <img src={arrow} alt="backward arrow" className={styles.arrow} />
            <p className={styles.backwardText}>Назад</p>
          </NavLink>
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
