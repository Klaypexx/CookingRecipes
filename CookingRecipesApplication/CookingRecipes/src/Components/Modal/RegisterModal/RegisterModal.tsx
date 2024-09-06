import RegisterForm from '../../Form/RegisterForm/RegisterForm';
import BaseModal from '../BaseModal/BaseModal';

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
