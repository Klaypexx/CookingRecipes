import BaseModal from '../BaseModal/BaseModal';
import styles from './AuthModal.module.css';
import LoginModal from '../LoginModal/LoginModal';
import RegisterModal from '../RegisterModal/RegisterModal';
import useModalStore from '../../../Stores/useModalStore';
import LinkBlock from '../../Link/LinkBlock/LinkBlock';

const AuthModal = () => {
  const { isAuth, isLogin, isRegister, setAuth, setLogin, setRegister } = useModalStore();
  const handlerLogin = () => {
    setLogin(isLogin);
    setAuth(isAuth);
  };

  const handlerRegister = () => {
    setRegister(isRegister);
    setAuth(isAuth);
  };

  return (
    <>
      <BaseModal haederText="Войдите в профиль">
        <p className={styles.authText}>Расширенные функции доступны только для зарегестрированных пользователей.</p>
        <LinkBlock
          linkPrimaryText="Войти"
          linkSecondaryText="Регистрация"
          onClickPrimary={handlerLogin}
          onClickSecondary={handlerRegister}
        />
      </BaseModal>

      {isLogin ? <LoginModal /> : null}

      {isRegister ? <RegisterModal /> : null}
    </>
  );
};

export default AuthModal;
