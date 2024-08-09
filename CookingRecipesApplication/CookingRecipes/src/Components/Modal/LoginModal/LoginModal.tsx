import BaseModal from '../BaseModal/BaseModal';
import LoginForm from '../../Form/LoginForm/LoginForm';

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
