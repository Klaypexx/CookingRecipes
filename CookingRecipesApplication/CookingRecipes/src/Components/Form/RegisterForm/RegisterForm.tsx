import AuthService from '../../../Services/AuthService';
import useModalStore from '../../../Stores/useModalStore';
import RegisterValues from '../../../Types/RegisterValues';
import ButtonBlock from '../../Button/ButtonBlock/ButtonBlock';
import BaseField from '../../Field/BaseField/BaseField';
import { successToast } from '../../Toast/Toast';
import BaseForm from '../BaseForm/BaseForm';
import styles from './RegisterForm.module.css';
import registerValidation from './RegisterValidaton';

const RegisterForm = () => {
  const { isLogin, setLogin, unsetAll } = useModalStore();

  const handleRegister = async (values: RegisterValues) => {
    await AuthService.register(values).then((res) => {
      if (res) {
        unsetAll();
        setLogin(isLogin);
        successToast('Вы успешно зарегестрировались!');
      }
    });
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
      <BaseForm primary initialValues={initialValues} validationSchema={registerValidation} onSubmit={handleRegister}>
        <BaseField margin name="name" type="text" labelText="Имя" />
        <BaseField margin name="username" type="text" labelText="Логин" />
        <div className={styles.smallnputBox}>
          <BaseField margin className={styles.smallInput} name="password" type="password" labelText="Пароль" />
          <BaseField
            className={styles.smallInput}
            name="confirmPassword"
            type="password"
            labelText="Повторите пароль"
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
