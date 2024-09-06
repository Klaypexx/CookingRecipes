import useAuthStore from '../../Stores/useAuthStore';
import useModalStore from '../../Stores/useModalStore';
import plusIcon from '../../resources/icons/plus-white.svg';
import homePageImage from '../../resources/img/headerPreview.png';
import BaseButton from '../Button/BaseButton/BaseButton';
import BaseLink from '../Link/BaseLink/BaseLink';
import styles from './Preview.module.css';

const Preview = () => {
  const { isLogin, setLogin } = useModalStore();
  const { isAuthorized } = useAuthStore();
  const handleLogin = () => {
    setLogin(isLogin);
  };

  return (
    <>
      <img src={homePageImage} alt="homePageImage" className={styles.homePageImage} />
      <div className={styles.previewContainer}>
        <h1>Готовь и делись рецептами</h1>
        <p className={styles.text}>Никаких кулинарных книг и блокнотов! Храни все любимые рецепты в одном месте.</p>
        <div className={styles.linksBox}>
          <BaseLink primary to="/recipes/create" text="Добавить рецепт">
            <img src={plusIcon} alt="plusImage" className={styles.plusIcon} />
          </BaseLink>
          {!isAuthorized && (
            <BaseButton buttonText="Войти" onClick={handleLogin} className={styles.loginButton}></BaseButton>
          )}
        </div>
      </div>
    </>
  );
};

export default Preview;
