import styles from './Preview.module.css';
import headerPreview from '../../resources/img/headerPreview.png';
import BaseLink from '../Link/BaseLink/BaseLink';
import useModalStore from '../../Stores/useModalStore';
import TokenService from '../../Services/TokenService';
import plusImg from '../../resources/icons/plus-white.svg';

const Preview = () => {
  const { isLogin, setLogin } = useModalStore();
  const token = TokenService.getAccessToken();
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
            <BaseLink primary navigation="/recipes/create" linkText="Добавить рецепт">
              <img src={plusImg} className={styles.plus} />
            </BaseLink>
            {token ? null : <BaseLink newStyle={{ width: '216px' }} linkText="Войти" onClick={handleLogin}></BaseLink>}
          </div>
        </div>
        <img src={headerPreview} alt="" className={styles.headerPreview} />
      </div>
    </section>
  );
};

export default Preview;
