import { MoonLoader } from 'react-spinners';
import styles from './Spinner.module.css';

const Spinner = () => {
  return (
    <>
      <div className={styles.spinnerContainer}>
        <MoonLoader color="rgb(253, 177, 0)" size={100} />
      </div>
    </>
  );
};

export default Spinner;
