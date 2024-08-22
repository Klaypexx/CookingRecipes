import useModalStore from '../../../Stores/useModalStore';
import AuthService from '../../../Services/AuthService';
import BaseForm from '../BaseForm/BaseForm';
import BaseField from '../../Field/BaseField/BaseField';
import ButtonBlock from '../../Button/ButtonBlock/ButtonBlock';
import loginValidation from './LoginValidation';
import LoginValues from '../../../Types/LoginValues';
import useAuthStore from '../../../Stores/useAuthStore';
import { successToast } from '../../Toast/Toast';

const LoginForm = () => {
  const { unsetAll } = useModalStore();
  const { setAuthorized } = useAuthStore();

  const handleLogin = async (values: LoginValues) => {
    const result = await AuthService.login(values);

    if (result.response && result.response.status === 200) {
      successToast('Вы успешно вошли в систему!');
      setAuthorized(true);
      unsetAll();
    }
  };

  const handleExit = () => {
    unsetAll();
  };

  const initialValues = {
    username: '',
    password: '',
  };

  return (
    <>
      <BaseForm primary initialValues={initialValues} validationSchema={loginValidation} onSubmit={handleLogin}>
        <BaseField margin name="username" type="text" placeholder="Логин" />
        <BaseField name="password" type="password" placeholder="Пароль" />
        <ButtonBlock
          primaryButtonText="Войти"
          secondaryButtonText="Отмена"
          primaryType="submit"
          onClickSecondary={handleExit}
        />
      </BaseForm>
    </>
  );
};

export default LoginForm;
