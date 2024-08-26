import styles from './Preview.module.css';
import headerPreview from '../../resources/img/headerPreview.png';
import BaseLink from '../Link/BaseLink/BaseLink';
import useModalStore from '../../Stores/useModalStore';
import plusImg from '../../resources/icons/plus-white.svg';
import BaseButton from '../Button/BaseButton/BaseButton';
import useAuthStore from '../../Stores/useAuthStore';

const Preview = () => {
  const { isLogin, setLogin } = useModalStore();
  const { isAuthorized } = useAuthStore();
  const handleLogin = () => {
    setLogin(isLogin);
  };

  return (
    <section className={styles.previewSection}>
      <div className={styles.previewContainer}>
        <div className={styles.previewBlock}>
          <h1 className={styles.headText}>Готовь и делись рецептами</h1>
          <p className={styles.subheadingText}>
            Никаких кулинарных книг и блокнотов! Храни все любимые рецепты в одном месте.
          </p>
          <div className={styles.links}>
            <BaseLink primary to="/recipes/create" linkText="Добавить рецепт">
              <img src={plusImg} className={styles.plus} />
            </BaseLink>
            {!isAuthorized && (
              <BaseButton newStyle={{ width: '216px' }} buttonText="Войти" onClick={handleLogin}></BaseButton>
            )}
          </div>
        </div>
        <img src={headerPreview} alt="" className={styles.headerPreview} />
      </div>
    </section>
  );
};

export default Preview;
