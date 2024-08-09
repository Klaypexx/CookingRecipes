import BaseModal from '../BaseModal/BaseModal';
import RegisterForm from '../../Form/RegisterForm/RegisterForm';

const RegisterModal = () => {
  return (
    <>
      <BaseModal primary haederText="Регистрация" hasAccountText="У меня уже есть аккаунт">
        <RegisterForm />
      </BaseModal>
    </>
  );
};

export default RegisterModal;
