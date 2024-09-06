import AuthService from '../../../Services/AuthService';
import useAuthStore from '../../../Stores/useAuthStore';
import useModalStore from '../../../Stores/useModalStore';
import LoginValues from '../../../Types/LoginValues';
import ButtonBlock from '../../Button/ButtonBlock/ButtonBlock';
import BaseField from '../../Field/BaseField/BaseField';
import { successToast } from '../../Toast/Toast';
import BaseForm from '../BaseForm/BaseForm';
import loginValidation from './LoginValidation';

const LoginForm = () => {
  const { unsetAll } = useModalStore();
  const { setAuthorized } = useAuthStore();

  const handleLogin = async (values: LoginValues) => {
    await AuthService.login(values).then((res) => {
      if (res) {
        successToast('Вы успешно вошли в систему!');
        setAuthorized(true);
        unsetAll();
      }
    });
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
        <BaseField margin name="username" type="text" labelText="Логин" />
        <BaseField name="password" type="password" labelText="Пароль" />
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
