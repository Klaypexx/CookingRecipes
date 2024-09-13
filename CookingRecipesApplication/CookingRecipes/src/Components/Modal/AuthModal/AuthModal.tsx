import useModalStore from '../../../Stores/useModalStore';
import LinkBlock from '../../Link/LinkBlock/LinkBlock';
import BaseModal from '../BaseModal/BaseModal';
import LoginModal from '../LoginModal/LoginModal';
import RegisterModal from '../RegisterModal/RegisterModal';
import styles from './AuthModal.module.css';

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
      <BaseModal haederText="Войдите в систему">
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
