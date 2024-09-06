import classNames from 'classnames';
import { useNavigate } from 'react-router-dom';
import arrowIcon from '../../resources/icons/backward-arrow.svg';
import SubheaderProps from '../../Types/SubheaderProps';
import styles from './Subheader.module.css';

const Subheader: React.FC<SubheaderProps> = ({ backward, text, children }) => {
  const navigate = useNavigate();

  const classNameBase = classNames(backward && styles.backward, styles.subheaderContainer);

  const handleNavigateToPreviousPage = () => navigate(-1);

  return (
    <div className={classNameBase}>
      {backward && (
        <div>
          <button onClick={handleNavigateToPreviousPage} className={styles.backwardBox}>
            <img src={arrowIcon} alt="arrowIcon" className={styles.arrowIcon} />
            <p className={styles.backwardText}>Назад</p>
          </button>
        </div>
      )}
      <div className={styles.subheadBox}>
        <h2>{text}</h2>
        {children && <>{children}</>}
      </div>
    </div>
  );
};

export default Subheader;
