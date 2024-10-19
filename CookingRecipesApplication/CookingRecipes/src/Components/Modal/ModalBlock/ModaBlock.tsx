import useModalStore from '../../../Stores/useModalStore';
import AuthModal from '../AuthModal/AuthModal';
import FilterModal from '../FilterModal/FilterModal';
import LoginModal from '../LoginModal/LoginModal';
import LogoutModal from '../LogoutModal/LogoutModal';
import RegisterModal from '../RegisterModal/RegisterModal';

const ModalBlock = () => {
  const { isRegister, isLogin, isAuth, isLogout, isFilter } = useModalStore();
  return (
    <>
      {isAuth ? <AuthModal /> : null}
      {isLogout ? <LogoutModal /> : null}
      {isLogin ? <LoginModal /> : null}
      {isRegister ? <RegisterModal /> : null}
      {isFilter ? <FilterModal /> : null}
    </>
  );
};

export default ModalBlock;
