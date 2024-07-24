import ButtonBlock from "../../Button/ButtonBlock/ButtonBlock";
import AuthService from "../../../Services/AuthService";
import useModalStore from "../../../Stores/useModalStore";
import BaseForm from "../../Form/BaseForm/BaseForm";
import FormInput from "../../Form/FormInput/FormInput";
import BaseModal from "../BaseModal/BaseModal"
import * as Yup from 'yup';
import styles from "./RegisterModal.module.css"
import { useState } from "react";
import { successToast } from "../../Toast/Toast";

interface RegisterValues {
    name: string,
    username: string,
    password: string
}


const RegisterModal = () => {
    const {isLogin, setLogin, unsetAll} = useModalStore();
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
            successToast("Вы успешно зарегестрировались!")
        } else {
            setErrorText(result.message); 
        }
    }

    const handleExit = () => {
        unsetAll();
    }

    const initialValues = {
        name: '',
        username: '',
        password: '',
        confirmPassword: '',
      };
    
      const validationSchema = Yup.object({
        name: Yup.string()
          .required('Имя обязательно')
          .min(3, 'Минимум 3 символа'),
        username: Yup.string()
          .required('Логин обязателен')
          .min(3, 'Минимум 3 символа'),
        password: Yup.string()
          .required('Пароль обязателен')
          .min(8, 'Минимум 8 символов')
          .max(25, 'Максимум 25 символов'),
        confirmPassword: Yup.string()
          .oneOf([Yup.ref('password')], 'Пароли должны совпадать')
          .required('Подтверждение пароля обязательно'),
      });

    return (
        <>
            <BaseModal 
                primary 
                haederText="Регистрация" 
                hasAccountText="У меня уже есть аккаунт" 
            >
                <BaseForm
                    primary
                    initialValues={initialValues}
                    validationSchema={validationSchema}
                    onSubmit={handleRegister}
                    errorText={errorText}
                >
                    <FormInput margin name="name" type="text" placeholder="Имя"/>
                    <FormInput margin name="username" type="text" placeholder="Логин"/>
                    <div className={styles.smallnputBox}>
                        <FormInput margin className={styles.smallInput} name="password" type="password" placeholder="Пароль"/>
                        <FormInput className={styles.smallInput} name="confirmPassword" type="password" placeholder="Повторит пароль"/>
                    </div>
                    <ButtonBlock 
                        primaryButtonText="Зарегистрировться" 
                        secondaryButtonText="Отмена" 
                        primaryType="submit" 
                        onClickSecondary={handleExit} />
                </BaseForm>

            </BaseModal>
        </>
    )
}

export default RegisterModal;