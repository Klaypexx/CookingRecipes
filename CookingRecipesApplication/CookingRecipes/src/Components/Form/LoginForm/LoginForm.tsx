import useModalStore from '../../../Stores/useModalStore';
import { useState } from 'react';
import { LoginValues } from '../../../Types/types';
import AuthService from '../../../Services/AuthService';
import { successToast } from '../../Toast/Toast';
import BaseForm from '../BaseForm/BaseForm';
import BaseField from '../../Field/BaseField/BaseField';
import ButtonBlock from '../../Button/ButtonBlock/ButtonBlock';
import loginValidation from './LoginValidation';

const LoginForm = () => {
  const { unsetAll } = useModalStore();
  const [errorText, setErrorText] = useState('');

  const handleLogin = async (values: LoginValues) => {
    const result = await AuthService.login(values.username, values.password);

    if (!result.success) {
      setErrorText(result.message);
      return;
    }

    if (result.response && result.response.status === 200) {
      successToast('Вы успешно вошли в систему!');
      unsetAll();
    } else {
      setErrorText(result.message);
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
      <BaseForm
        primary
        initialValues={initialValues}
        validationSchema={loginValidation}
        onSubmit={handleLogin}
        errorText={errorText}
      >
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
