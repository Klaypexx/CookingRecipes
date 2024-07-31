import useModalStore from '../../../Stores/useModalStore';
import { useState } from 'react';
import { RegisterValues } from '../../../Types/types';
import AuthService from '../../../Services/AuthService';
import { successToast } from '../../Toast/Toast';
import BaseForm from '../BaseForm/BaseForm';
import BaseField from '../../Field/BaseField/BaseField';
import styles from './RegisterForm.module.css';
import ButtonBlock from '../../Button/ButtonBlock/ButtonBlock';
import registerValidation from './RegisterValidaton';

const RegisterForm = () => {
  const { isLogin, setLogin, unsetAll } = useModalStore();
  const [errorText, setErrorText] = useState('');

  const handleRegister = async (values: RegisterValues) => {
    const result = await AuthService.register(values.name, values.username, values.password);

    if (!result.success) {
      setErrorText(result.message);
      return;
    }

    if (result.response && result.response.status === 200) {
      unsetAll();
      setLogin(isLogin);
      successToast('Вы успешно зарегестрировались!');
    } else {
      setErrorText(result.message);
    }
  };

  const handleExit = () => {
    unsetAll();
  };

  const initialValues = {
    name: '',
    username: '',
    password: '',
    confirmPassword: '',
  };

  return (
    <>
      <BaseForm
        primary
        initialValues={initialValues}
        validationSchema={registerValidation}
        onSubmit={handleRegister}
        errorText={errorText}
      >
        <BaseField margin name="name" type="text" placeholder="Имя" />
        <BaseField margin name="username" type="text" placeholder="Логин" />
        <div className={styles.smallnputBox}>
          <BaseField margin className={styles.smallInput} name="password" type="password" placeholder="Пароль" />
          <BaseField
            className={styles.smallInput}
            name="confirmPassword"
            type="password"
            placeholder="Повторит пароль"
          />
        </div>
        <ButtonBlock
          primaryButtonText="Зарегистрировться"
          secondaryButtonText="Отмена"
          primaryType="submit"
          onClickSecondary={handleExit}
        />
      </BaseForm>
    </>
  );
};

export default RegisterForm;
