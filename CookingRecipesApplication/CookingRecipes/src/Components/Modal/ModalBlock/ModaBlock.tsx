import useModalStore from '../../../Stores/useModalStore';
import AuthModal from '../AuthModal/AuthModal';
import LoginModal from '../LoginModal/LoginModal';
import LogoutModal from '../LogoutModal/LogoutModal';
import RegisterModal from '../RegisterModal/RegisterModal';

const ModalBlock = () => {
  const { isRegister, isLogin, isAuth, isLogout } = useModalStore();
  return (
    <>
      {isAuth ? <AuthModal /> : null}
      {isLogout ? <LogoutModal /> : null}
      {isLogin ? <LoginModal /> : null}
      {isRegister ? <RegisterModal /> : null}
    </>
  );
};

export default ModalBlock;
