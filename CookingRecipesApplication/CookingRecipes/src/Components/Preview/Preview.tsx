import { useNavigate } from 'react-router-dom';
import useAuthStore from '../../Stores/useAuthStore';
import useModalStore from '../../Stores/useModalStore';
import plusIcon from '../../resources/icons/plus-white.svg';
import homePageImage from '../../resources/img/headerPreview.png';
import BaseButton from '../Button/BaseButton/BaseButton';
import { warnToast } from '../Toast/Toast';
import styles from './Preview.module.css';

const Preview = () => {
  const { isLogin, setLogin } = useModalStore();
  const { isAuth, setAuth } = useModalStore();
  const { isAuthorized } = useAuthStore();
  const navigation = useNavigate();

  const handleLogin = () => {
    setLogin(isLogin);
  };

  const onButtonClick = () => {
    if (!isAuthorized) {
      warnToast('Вы не вошли в систему');
      setAuth(isAuth);
      return;
    }
    navigation('/recipes/create');
  };

  return (
    <>
      <img src={homePageImage} alt="homePageImage" className={styles.homePageImage} />
      <div className={styles.previewContainer}>
        <h1>Готовь и делись рецептами</h1>
        <p className={styles.text}>Никаких кулинарных книг и блокнотов! Храни все любимые рецепты в одном месте.</p>
        <div className={styles.linksBox}>
          <BaseButton primary buttonText="Добавить рецепт" onClick={onButtonClick}>
            <img src={plusIcon} alt="plusImage" className={styles.plusIcon} />
          </BaseButton>
          {!isAuthorized && (
            <BaseButton buttonText="Войти" onClick={handleLogin} className={styles.loginButton}></BaseButton>
          )}
        </div>
      </div>
    </>
  );
};

export default Preview;
