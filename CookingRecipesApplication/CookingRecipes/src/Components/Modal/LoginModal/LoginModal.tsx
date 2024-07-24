import ButtonBlock from "../../Button/ButtonBlock/ButtonBlock";
import AuthService from "../../../Services/AuthService";
import useModalStore from "../../../Stores/useModalStore";
import BaseForm from "../../Form/BaseForm/BaseForm";
import FormInput from "../../Form/FormInput/FormInput";
import BaseModal from "../BaseModal/BaseModal"
import * as Yup from 'yup';
import { useState } from "react";
import { successToast } from "../../Toast/Toast";

interface LoginValues {
    username: string,
    password: string
}

const LoginModal = () => {
    const {unsetAll} = useModalStore();
    const [errorText, setErrorText] = useState('');


    const handleLogin = async (values: LoginValues) => {
        const result = await AuthService.login(values.username, values.password);
        
        if (!result.success) {
            setErrorText(result.message); 
            return;
        }

        if (result.response && result.response.status === 200) {
            successToast("Вы успешно вошли в систему!");
            unsetAll();
        } else {
            setErrorText(result.message); 
        }
    };
    
    const handleExit = () => {
        unsetAll();
    }
    
    const initialValues = {
        username: '',
        password: '',
    };

    const validationSchema = Yup.object({
        username: Yup.string()
          .required('Логин обязателен')
          .min(3, 'Минимум 3 символа')
          .max(25, 'Максимум 25 символов'),
        password: Yup.string()
          .required('Пароль обязателен')
          .min(8, 'Минимум 8 символов')
          .max(25, 'Максимум 25 символов'),
      });

    return (
        <>
            <BaseModal 
                primary 
                haederText="Войти" 
                hasAccountText="У меня еще нет аккаунта"  
            >
                    <BaseForm
                        primary
                        initialValues={initialValues}
                        validationSchema={validationSchema}
                        onSubmit={handleLogin}
                        errorText={errorText}
                    >
                        <FormInput margin name="username" type="text" placeholder="Логин"/>
                        <FormInput name="password" type="password" placeholder="Пароль"/>
                        <ButtonBlock primaryButtonText="Войти" secondaryButtonText="Отмена" primaryType="submit" onClickSecondary={handleExit} />
                    </BaseForm>
            </BaseModal>
        </>
    )
}

export default LoginModal;