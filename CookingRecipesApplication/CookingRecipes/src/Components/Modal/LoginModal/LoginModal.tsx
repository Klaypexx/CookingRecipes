import LoginForm from '../../Form/LoginForm/LoginForm';
import BaseModal from '../BaseModal/BaseModal';

const LoginModal = () => {
  return (
    <>
      <BaseModal primary haederText="Войти" hasAccountText="У меня еще нет аккаунта">
        <LoginForm />
      </BaseModal>
    </>
  );
};

export default LoginModal;
