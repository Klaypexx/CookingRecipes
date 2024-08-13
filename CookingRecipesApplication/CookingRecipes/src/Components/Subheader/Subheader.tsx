import classNames from 'classnames';
import styles from './Subheader.module.css';
import arrow from '../../resources/icons/backward-arrow.svg';
import { useNavigate } from 'react-router-dom';
import SubheaderProps from '../../Types/SubheaderProps';

const Subheader: React.FC<SubheaderProps> = ({ backward, headerText, children }) => {
  const navigate = useNavigate();

  const classList = classNames(backward && styles.backward, styles.subheaderContainer);

  const handleGoBack = () => navigate(-1);

  return (
    <div className={classList}>
      {backward && (
        <div className={styles.backwardContainer}>
          <button onClick={handleGoBack} className={styles.backwardBox}>
            <img src={arrow} alt="backward arrow" className={styles.arrow} />
            <p className={styles.backwardText}>Назад</p>
          </button>
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
