import classNames from 'classnames';
import styles from './Subheader.module.css';
import arrow from '../../resources/icons/backward-arrow.svg';
import { NavLink } from 'react-router-dom';
import BaseButton from '../Button/BaseButton/BaseButton';
import { SubheaderProps } from '../../Types/types';

const Subheader: React.FC<SubheaderProps> = ({ backward, type, headerText, btn, buttonText, onClick, children }) => {
  const classList = classNames(backward ? styles.backward : undefined, styles.subheaderContainer);

  return (
    <div className={classList}>
      {backward && (
        <NavLink to={'/'} state={{ from: location.pathname }} className={styles.backwardBox}>
          <img src={arrow} alt="backward arrow" className={styles.arrow} />
          <p className={styles.backwardText}>Назад</p>
        </NavLink>
      )}
      <div className={styles.subheadBox}>
        <h2>{headerText}</h2>
        {btn && (
          <BaseButton primary type={type} buttonText={buttonText} onClick={onClick}>
            {children}
          </BaseButton>
        )}
      </div>
    </div>
  );
};

export default Subheader;
